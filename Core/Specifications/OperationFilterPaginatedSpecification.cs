using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications
{
    public class OperationFilterPaginatedSpecification : Specification<Operation>
    {
        public OperationFilterPaginatedSpecification(int skip, int take)
            : base()
        {
            Query.Paginate(skip, take);
        }
    }
}
