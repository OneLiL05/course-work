using trade_compas.Interfaces.Basic;
using trade_compas.Models;
using trade_compas.Utilities.DTOs.Comment;

namespace trade_compas.Interfaces.Repositories;

public interface ICommentsRepository : IBaseRepository<Comment>, ICreatable<CreateCommentDto>, IDeletable, IGetableBy<Comment>
{
}