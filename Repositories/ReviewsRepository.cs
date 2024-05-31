using trade_compas.Interfaces.Helpers;
using trade_compas.Interfaces.Repositories;
using trade_compas.Models;
using trade_compas.Utilities.Actions;
using trade_compas.Utilities.DTOs.Comment;

namespace trade_compas.Repositories;

public class ReviewsRepository(IPathHelper pathHelper, IProductsRepository productsRepository) : IReviewsRepository
{
    private readonly string _collectionPath = pathHelper.GetCollectionPath("reviews");
    private readonly GetAllAction<Review> _getAllAction = new();
    private readonly GetOneAction<Review> _getOneAction = new();
    private readonly CreateAction<Review> _createAction = new();
    private readonly GetAllByAction<Review> _getAllByAction = new();
    private readonly DeleteAction<Review> _deleteAction = new();

    public List<Review> GetAll()
    {
        return _getAllAction.DoAction(_collectionPath);
    }

    public Review? GetOne(Predicate<Review> match)
    {
        return _getOneAction.DoAction(GetAll(), match);
    }

    public void CreateOne(CreateCommentDto dto)
    {
        var comment = new Review(dto);

        _createAction.DoAction(_collectionPath, comment);

        productsRepository.AddReview(dto.ProductId, comment);
    }

    public void DeleteOne(int id)
    {
        var comment = GetOne(comment => comment.Id == id);

        Console.WriteLine(id);

        productsRepository.RemoveReview(comment.ProductId, comment);

        _deleteAction.DoAction(_collectionPath, id);
    }

    public List<Review> GetAllBy(Predicate<Review> match)
    {
        return _getAllByAction.DoAction(GetAll(), match);
    }
}