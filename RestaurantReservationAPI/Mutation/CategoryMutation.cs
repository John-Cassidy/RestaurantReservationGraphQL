using System;
using GraphQL;
using GraphQL.Types;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Models;
using RestaurantReservationAPI.Type;

namespace RestaurantReservationAPI.Mutation;

public class CategoryMutation : ObjectGraphType
{
    public CategoryMutation(ICategoryRepository categoryRepository)
    {
        // Add Fields to Create, Update and Delete Category using the CategoryRepository and .Arguments() and .Resolve() methods
        Field<CategoryType>("CreateCategory")
            .Arguments(new QueryArguments(new QueryArgument<CategoryInputType> { Name = "category" }))
            .Resolve(context =>
            {
                var category = context.GetArgument<Category>("category");
                return categoryRepository.AddCategory(category);
            });

        Field<CategoryType>("UpdateCategory")
            .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryId" },
                new QueryArgument<CategoryInputType> { Name = "category" }
            ))
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("categoryId");
                var category = context.GetArgument<Category>("category");
                return categoryRepository.UpdateCategory(id, category);
            });

        Field<StringGraphType>("DeleteCategory")
            .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryId" }))
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("categoryId");
                categoryRepository.DeleteCategory(id);
                return $"The category with the id: {id} has been successfully deleted.";
            });
    }

}
