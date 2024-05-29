using trade_compas.Interfaces.Helpers;
using trade_compas.Interfaces.Repositories;
using trade_compas.Models;
using trade_compas.Utilities.Actions;
using trade_compas.Utilities.DTOs.Comment;

namespace trade_compas.Repositories;

public class CommentsRepository(IPathHelper pathHelper, IProductsRepository productsRepository) : ICommentsRepository
{
    private readonly string _collectionPath = pathHelper.GetCollectionPath("comments");
    private readonly GetAllAction<Comment> _getAllAction = new();
    private readonly GetOneAction<Comment> _getOneAction = new();
    private readonly CreateAction<Comment> _createAction = new();
    private readonly DeleteAction<Comment> _deleteAction = new();
    private readonly GetAllByAction<Comment> _getAllByAction = new();
    private readonly UpdateAction<Comment> _updateAction = new();

    public List<Comment> GetAll()
    {
        return _getAllAction.DoAction(_collectionPath);
    }

    public Comment? GetOne(Predicate<Comment> match)
    {
        return _getOneAction.DoAction(GetAll(), match);
    }

    public void CreateOne(CreateCommentDto dto)
    {
        var comment = new Comment(dto);

        _createAction.DoAction(_collectionPath, comment);

        productsRepository.AddComment(dto.ProductId, comment);
    }

    public void UpdateOne(int id, UpdateCommentDto dto)
    {
        _updateAction.DoAction(
            _collectionPath,
            comment => comment.Id == id,
            comment =>
            {
                comment.Content = dto.Content;
                comment.Grade = dto.Grade;
            });
    }

    public void DeleteOne(int id)
    {
        _deleteAction.DoAction(_collectionPath, id);
    }

    public List<Comment> GetAllBy(Predicate<Comment> match)
    {
        return _getAllByAction.DoAction(GetAll(), match);
    }

}