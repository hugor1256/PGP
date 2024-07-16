using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PGP.Helpers;
using PGP.Repository.Context;

namespace PGP.Repository.Repositories.Base;

public class ExpressionBuilderKey<T> where T : class
{
    public ExpressionBuilderKey(string key, Expression<Func<T, bool>> expression)
    {
        Key = key;
        this.Expression = expression;
    }

    public string Key { get; set; }
    public Expression<Func<T, bool>> Expression { get; set; }
}

public class RepositoryBase<T> where T : class
    {
        private readonly PgpContext _pgpContext;
        private readonly IList<Expression<Func<T, bool>>> _predicatesWhere = new List<Expression<Func<T, bool>>>();
        private readonly IList<ExpressionBuilderKey<T>> _predicatesBuildersWhere = new List<ExpressionBuilderKey<T>>();

        private readonly IList<string> _includesTable = new List<string>();
        
        private DateTime? _dataTemporal = null;

        private readonly IList<Expression<Func<T, object>>>
            _predicatesOrderBy = new List<Expression<Func<T, object>>>();

        private readonly IList<Expression<Func<T, object>>> _predicatesOrderByDescending =
            new List<Expression<Func<T, object>>>();

        private readonly ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
        private int _numeroDaPagina = 0;
        private int _registroPorPagina = 30;
        private bool _paginar = false;
        private int _totalDeRegistros = 0;

        protected RepositoryBase(PgpContext pgpContext)
        {
            _pgpContext = pgpContext;
        }

        protected void AdicionarPredicateBuilderWhere(string key, Expression<Func<T, bool>> predicate) =>
            _predicatesBuildersWhere.Add(new ExpressionBuilderKey<T>(key, predicate));

        private void RemoverPredicateBuilderWhere()
        {
            while (_predicatesBuildersWhere.Count > 0)
            {
                _predicatesBuildersWhere.RemoveAt(0);
            }
        }

        protected void AdicionarPredicateWhere(Expression<Func<T, bool>> predicate) => _predicatesWhere.Add(predicate);

        private void RemoverPredicateWhere()
        {
            while (_predicatesWhere.Count > 0)
            {
                _predicatesWhere.RemoveAt(0);
            }
        }
        protected void AdicionarPredicateOrderBy(Expression<Func<T, object>> predicate) =>
            _predicatesOrderBy.Add(predicate);

        protected void AdicionarPredicateOrderByDescending(Expression<Func<T, object>> predicate) =>
            _predicatesOrderByDescending.Add(predicate);

        protected void LimparPredicatesWhere()
        {
            RemoverPredicateBuilderWhere();

            RemoverPredicateWhere();
        }

        private IQueryable<T> SetQueryable()
        {
            if (!_dataTemporal.HasValue) return _pgpContext.Set<T>();

            if (!_includesTable.Any()) return _pgpContext.Set<T>().TemporalAsOf(_dataTemporal.Value);

            return _includesTable.Aggregate(_pgpContext.Set<T>().TemporalAsOf(_dataTemporal.Value),
                    (current, include) => current.Include(include));
        }

        protected Expression<Func<T, bool>> ObterPredicatesBuilders()
        {
            var expressionByKey = _predicatesBuildersWhere.GroupBy(a => a.Key).ToList();

            if (expressionByKey.Count == 1)
            {
                var oneExpression = expressionByKey.First();

                if (oneExpression.Count() == 1)
                {
                    return oneExpression.First().Expression;
                }

                return Expression.Lambda<Func<T, bool>>(ObterLambdaPredicatesBuilders(_predicatesBuildersWhere),
                    parameterExpression);
            }

            Expression finalExpression = Expression.Constant(true);

            foreach (var keyExpression in expressionByKey)
            {
                finalExpression = Expression.AndAlso(
                    finalExpression,
                    ObterLambdaPredicatesBuilders(_predicatesBuildersWhere
                            .Where(e => e.Key.Equals(keyExpression.Key)).ToList()));
            }

            return Expression.Lambda<Func<T, bool>>(finalExpression, parameterExpression);
        }

        protected Expression ObterLambdaPredicatesBuilders(IList<ExpressionBuilderKey<T>> listaDePredicates)
        {
            if (listaDePredicates.Count == 1)
                return Expression.Invoke(listaDePredicates.First().Expression, parameterExpression);

            if (listaDePredicates.Count == 2)
            {
                return BinaryExpression.OrElse(
                    Expression.Invoke(listaDePredicates.First().Expression, parameterExpression),
                    Expression.Invoke(listaDePredicates.Last().Expression, parameterExpression)
                );
            }

            return BinaryExpression.OrElse(
                ObterLambdaPredicatesBuilders(listaDePredicates.Skip(1).ToList()),
                Expression.Invoke(listaDePredicates.First().Expression, parameterExpression)
            );
        }

        public IQueryable<T> ObterTodos()
        {
            IQueryable<T> query = SetQueryable();

            if (_predicatesBuildersWhere.Any())
                query = query.Where(ObterPredicatesBuilders());

            foreach (var predicate in _predicatesWhere)
                query = query.Where(predicate);

            foreach (var predicate in _predicatesOrderBy)
                query = query.SmartOrderBy(predicate);

            foreach (var predicate in _predicatesOrderByDescending)
                query = query.SmartOrderByDescending(predicate);

            _totalDeRegistros = query.Count();

            if (_paginar)
            {
                return query.Skip(_numeroDaPagina).Take(_registroPorPagina);
            }

            return query;
        }

        public T? ObterPorId(object id)
        {
            return _pgpContext.Set<T>().Find(id);
        }

        public T? ObterPorIdPorPredicate(Expression<Func<T, bool>> predicate)
        {
            return ObterPorPredicato(predicate).FirstOrDefault();
        }

        public IQueryable<T> ObterPorPredicato(Expression<Func<T, bool>> predicate)
        {
            return SetQueryable().Where(predicate);
        }

        public bool Existe(Expression<Func<T, bool>> predicate)
        {
            return SetQueryable().Any(predicate);
        }

        public virtual void Inserir(T entity)
        {
            _pgpContext.Add(entity);
        }

        public void InserirLista(List<T> entity)
        {
            _pgpContext.AddRange(entity);
        }

        public virtual void Remover(T entity)
        {
            _pgpContext.Remove(entity);
        }

        public void RemoverLista(List<T> entity)
        {
            _pgpContext.RemoveRange(entity);
        }

        public void Paginar(int pagina, int registroPorPagina)
        {
            _numeroDaPagina = (pagina - 1) * registroPorPagina;
            _registroPorPagina = registroPorPagina;
            _paginar = true;
        }

        public int TotalDeRegistros() => _totalDeRegistros;

        public int TotalDePaginas()
        {
            if (_totalDeRegistros == 0)
                return 1;

            return Convert.ToInt32(
                Math.Truncate((decimal)_totalDeRegistros / (decimal)_registroPorPagina) +
                Convert.ToInt32(((decimal)_totalDeRegistros % (decimal)_registroPorPagina) > 0));
        }

        public void Atualizar(T entity)
        {
            _pgpContext.Update(entity);
        }

        public void Salvar()
        {
            if (_pgpContext.ChangeTracker.HasChanges())
            {
                _pgpContext.SaveChanges();
            }
        }

        public async Task SalvarAsync()
        {
            if (_pgpContext.ChangeTracker.HasChanges())
            {
                await _pgpContext.SaveChangesAsync();
                _pgpContext.ChangeTracker.Clear();
            }
        }
    }