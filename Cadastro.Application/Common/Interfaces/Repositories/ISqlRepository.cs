using System.Linq.Expressions;

namespace Cadastro.Application.Common.Interfaces.Repositories
{
    public interface ISqlRepository<TDocument> where TDocument : class
    {
        void InsertOne(TDocument document, CancellationToken cancellationToken);
        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken);
    }
}
