using AnimeFigureProject.DatabaseContext.Data;
using AnimeFigureProject.EntityModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AnimeFigureProject.DatabaseAccess
{
    
    public class DataAccessService
    {

        private readonly ApplicationDbContext? applicationContext;

        public DataAccessService(ApplicationDbContext applicationContext)
        {
            
            this.applicationContext = applicationContext;

        }

        #region Collector

        /// <summary>
        /// Gets specific collector data.
        /// </summary>
        /// <param name="id">Id of the specific collector</param>
        /// <returns>The specific collector</returns>
        public async Task<Collector?> GetCollector(int id)
        {

            if (applicationContext?.Collectors == null)
                return null;

            return await applicationContext.Collectors.FirstOrDefaultAsync(c => c.Id == id);

        }

        /// <summary>
        /// Gets specific collector data based on authentication user id.
        /// </summary>
        /// <param name="authenticationUserId">The id of the user authentication</param>
        /// <returns>The specific collector</returns>
        public async Task<Collector?> GetCollector(string authenticationUserId)
        {

            if (applicationContext?.Collectors == null)
                return null;

            return await applicationContext.Collectors.FirstOrDefaultAsync(c => c.AuthenticationUserId == authenticationUserId);

        }

        /// <summary>
        /// Creates new collector.
        /// </summary>
        /// <param name="collector">The new collector to be created</param>
        /// <returns>New collector that was created</returns>
        public async Task<Collector?> CreateCollector(Collector collector)
        {

            if (applicationContext?.Collectors == null)
                return null;

            applicationContext.Collectors?.AddAsync(collector);
            await applicationContext.SaveChangesAsync();

            return collector;

        }

        /// <summary>
        /// Updates collector data.
        /// </summary>
        /// <param name="animeFigure">The updated collector</param>
        /// <returns>The updated collector</returns>
        public async Task<Collector?> UpdateCollector(Collector collector)
        {

            if (applicationContext?.Collectors == null)
                return null;

            applicationContext.Collectors.Update(collector);
            await applicationContext.SaveChangesAsync();

            return collector;

        }

        /// <summary>
        /// Deletes specific collector and its colections.
        /// </summary>
        /// <param name="id">Id of the collector to be deleted</param>
        /// <returns>Deletion was succesfull</returns>
        public async Task<bool> DeleteCollector(int id)
        {

            if (applicationContext?.Collectors == null)
                return false;
             
            Collector? collector = applicationContext.Collectors.FirstOrDefault(c => c.Id == id);

            if (collector == null)
                return false;

            if (applicationContext.Collections != null)
            {

                List<Collection> collections = await applicationContext.Collections.Where(c => c.OwnerId == id).ToListAsync();
                applicationContext.Collections.RemoveRange(collections);
                await applicationContext.SaveChangesAsync();

            }

            applicationContext.Collectors.Remove(collector);
            await applicationContext.SaveChangesAsync();

            return true;

        }

        #endregion

        #region Anime figures

        /// <summary>
        /// Gets list of all anime figures.
        /// </summary>
        /// <returns>List of all anime figures in database</returns>
        public async Task<List<AnimeFigure>?> GetAllAnimeFigures()
        {

            if (applicationContext?.AnimeFigures == null)
                return null;

            return await applicationContext.AnimeFigures.ToListAsync();

        }

        /// <summary>
        /// Gets filtered list of all anime figures.
        /// </summary>
        /// <param name="searchTerm">Name of anime figure</param>
        /// <param name="brandIds">The brands that you want to see the anime figures from</param>
        /// <param name="CategoryIds">The category that you want to see the anime figures from</param>
        /// <param name="originIds">The origins that you want to see the anime figures from</param>
        /// <returns>List of filtered anime figures</returns>
        public async Task<List<AnimeFigure>?> GetFilteredAnimeFigures(string? searchTerm, int[]? brandIds, int[]? CategoryIds, int[]? originIds, int[]? YearOfReleases)
        {

            if (applicationContext?.AnimeFigures == null)
                return null;

            IQueryable<AnimeFigure> animeFigures = applicationContext.AnimeFigures;

            if (animeFigures == null)
                return null;

            if (!string.IsNullOrEmpty(searchTerm))
                animeFigures = animeFigures.Where(f => f.Name != null && f.Name.Contains(searchTerm));

            if (brandIds != null && brandIds.Length > 0)
                animeFigures = animeFigures.Where(f => f.Brand != null && brandIds.Contains(f.Brand.Id));

            if (CategoryIds != null && CategoryIds.Length > 0)
                animeFigures = animeFigures.Where(f => f.Category != null && CategoryIds.Contains(f.Category.Id));

            if (originIds != null && originIds.Length > 0)
            {

                var filteredFigures = animeFigures.Include(f => f.Origins).AsEnumerable().Where(f => f.Origins != null && f.Origins.Any(origin => originIds.Contains(origin.Id))).Select(f => f.Id);
                animeFigures = animeFigures.Where(f => filteredFigures.Contains(f.Id));

            }

            if (YearOfReleases != null && YearOfReleases.Length > 0)
                animeFigures = animeFigures.Where(f => f.YearOfRelease != null && YearOfReleases.Contains(f.YearOfRelease.Value));

            return await animeFigures.ToListAsync();

        }

        /// <summary>
        /// Gets specific anime figure.
        /// </summary>
        /// <param name="id">Id of specific anime figure to get</param>
        /// <returns>Specific anime figure</returns>
        public async Task<AnimeFigure?> GetAnimeFigure(int id)
        {

            if (applicationContext?.AnimeFigures == null)
                return null;

            return await applicationContext.AnimeFigures.SingleOrDefaultAsync(f => f.Id == id);

        }

        /// <summary>
        /// Creates new anime figure.
        /// </summary>
        /// <param name="animeFigure">The new anime figure to create</param>
        /// <returns>New anime figure that was created</returns>
        public async Task<AnimeFigure?> CreateAnimeFigure(AnimeFigure animeFigure)
        {

            if (applicationContext?.AnimeFigures == null)
                return null;

            applicationContext.AnimeFigures?.AddAsync(animeFigure);
            await applicationContext.SaveChangesAsync();

            return animeFigure;

        }

        /// <summary>
        /// Updates anime figure data.
        /// </summary>
        /// <param name="animeFigure">The updated anime figure</param>
        /// <returns>The updated anime figure</returns>
        public async Task<AnimeFigure?> UpdateAnimeFigure(AnimeFigure animeFigure)
        {

            if (applicationContext?.AnimeFigures == null)
                return null;

            applicationContext.AnimeFigures.Update(animeFigure);
            await applicationContext.SaveChangesAsync();

            return animeFigure;

        }

        #endregion

        #region Brand

        /// <summary>
        /// Gets all brands in database.
        /// </summary>
        /// <returns>List of brands in database</returns>
        public async Task<List<Brand>?> GetBrands()
        {

            if (applicationContext?.Brands == null)
                return null;

            return await applicationContext.Brands.ToListAsync();

        }

        /// <summary>
        /// Gets all brands specified in id list.
        /// </summary>
        /// <param name="ids">Ids of specific brands</param>
        /// <returns>List of brands with id's from id list</returns>
        public async Task<List<Brand>?> GetBrands(int[] ids)
        {

            if (applicationContext?.Brands == null)
                return null;

            return await applicationContext.Brands.Where(b => ids.Contains(b.Id)).ToListAsync();

        }

        /// <summary>
        /// Gets specific brand.
        /// </summary>
        /// <param name="id">Id of specific brand</param>
        /// <returns>Specific brand</returns>
        public async Task<Brand?> GetBrand(int id)
        {

            if (applicationContext?.Brands == null)
                return null;

            return await applicationContext.Brands.FirstOrDefaultAsync(b => b.Id == id);

        }

        /// <summary>
        /// Gets specific brand.
        /// </summary>
        /// <param name="name">Name of specific brand</param>
        /// <returns>Specific brand</returns>
        public async Task<Brand?> GetBrand(string name)
        {

            if (applicationContext?.Brands == null)
                return null;

            return await applicationContext.Brands.FirstOrDefaultAsync(b => b.Name == name);

        }

        /// <summary>
        /// Creates new brand.
        /// </summary>
        /// <param name="brand">The new brand to create</param>
        /// <returns>New brand that was created</returns>
        public async Task<Brand?> CreateBrand(Brand brand)
        {

            if (applicationContext?.Brands == null)
                return null;

            applicationContext.Brands?.AddAsync(brand);
            await applicationContext.SaveChangesAsync();

            return brand;

        }

        /// <summary>
        /// Updates brand data.
        /// </summary>
        /// <param name="brand">The update brand</param>
        /// <returns>The update brand</returns>
        public async Task<Brand?> UpdateBrand(Brand brand)
        {

            if (applicationContext?.Brands == null)
                return null;

            applicationContext.Brands.Update(brand);
            await applicationContext.SaveChangesAsync();

            return brand;

        }

        #endregion

        #region Origins

        /// <summary>
        /// Gets all origins in database.
        /// </summary>
        /// <returns>List of origins in database</returns>
        public async Task<List<Origin>?> GetOrigins()
        {

            if (applicationContext?.Origins == null)
                return null;

            return await applicationContext.Origins.ToListAsync();

        }

        /// <summary>
        /// Gets all origins specified in id list.
        /// </summary>
        /// <param name="ids">Ids of specific origins</param>
        /// <returns>List of origins with id's from id list</returns>
        public async Task<List<Origin>?> GetOrigins(int[] ids)
        {

            if (applicationContext?.Origins == null)
                return null;

            return await applicationContext.Origins.Where(o => ids.Contains(o.Id)).ToListAsync();

        }

        /// <summary>
        /// Gets specific origin.
        /// </summary>
        /// <param name="id">Id of specific origin</param>
        /// <returns>Specific origin</returns>
        public async Task<Origin?> GetOrigin(int id)
        {

            if (applicationContext?.Origins == null)
                return null;

            return await applicationContext.Origins.FirstOrDefaultAsync(o => o.Id == id);

        }

        /// <summary>
        /// Gets specific origin.
        /// </summary>
        /// <param name="name">Name of specific origin</param>
        /// <returns>Specific origin</returns>
        public async Task<Origin?> GetOrigin(string name)
        {

            if (applicationContext?.Origins == null)
                return null;

            return await applicationContext.Origins.FirstOrDefaultAsync(o => o.Name == name);

        }

        /// <summary>
        /// Creates new origin.
        /// </summary>
        /// <param name="origin">The new origin to create</param>
        /// <returns>New origin that was created</returns>
        public async Task<Origin?> CreateOrigin(Origin origin)
        {

            if (applicationContext?.Origins == null)
                return null;

            applicationContext.Origins?.AddAsync(origin);
            await applicationContext.SaveChangesAsync();

            return origin;

        }

        /// <summary>
        /// Updates origin data.
        /// </summary>
        /// <param name="origin">The update origin</param>
        /// <returns>The update origin</returns>
        public async Task<Origin?> UpdateOrigin(Origin origin)
        {

            if (applicationContext?.Origins == null)
                return null;

            applicationContext.Origins.Update(origin);
            await applicationContext.SaveChangesAsync();

            return origin;

        }

        #endregion

        #region Category

        /// <summary>
        /// Gets all categories in database.
        /// </summary>
        /// <returns>List of categories in database</returns>
        public async Task<List<Category>?> GetCategories()
        {

            if (applicationContext?.Categories == null)
                return null;

            return await applicationContext.Categories.ToListAsync();

        }

        /// <summary>
        /// Gets all categories specified in id list.
        /// </summary>
        /// <param name="ids">Ids of specific origins</param>
        /// <returns>List of categories with id's from id list</returns>
        public async Task<List<Category>?> GetCategories(int[] ids)
        {

            if (applicationContext?.Categories == null)
                return null;

            return await applicationContext.Categories.Where(c => ids.Contains(c.Id)).ToListAsync();

        }

        /// <summary>
        /// Gets specific category.
        /// </summary>
        /// <param name="id">Id of specific category</param>
        /// <returns>Specific category</returns>
        public async Task<Category?> GetCategory(int id)
        {

            if (applicationContext?.Categories == null)
                return null;

            return await applicationContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        }

        /// <summary>
        /// Gets specific category.
        /// </summary>
        /// <param name="name">Name of specific category</param>
        /// <returns>Specific category</returns>
        public async Task<Category?> GetCategory(string name)
        {

            if (applicationContext?.Categories == null)
                return null;

            return await applicationContext.Categories.FirstOrDefaultAsync(c => c.Name == name);

        }

        /// <summary>
        /// Creates new category.
        /// </summary>
        /// <param name="category">The new category to create</param>
        /// <returns>New category that was created</returns>
        public async Task<Category?> CreateCategory(Category category)
        {

            if (applicationContext?.Categories == null)
                return null;

            applicationContext.Categories?.AddAsync(category);
            await applicationContext.SaveChangesAsync();

            return category;

        }

        /// <summary>
        /// Updates category data.
        /// </summary>
        /// <param name="category">The update category</param>
        /// <returns>The update category</returns>
        public async Task<Category?> UpdateCategory(Category category)
        {

            if (applicationContext?.Categories == null)
                return null;

            applicationContext.Categories.Update(category);
            await applicationContext.SaveChangesAsync();

            return category;

        }

        #endregion

        #region Review

        /// <summary>
        /// Gets all reviews in database.
        /// </summary>
        /// <returns>List of reviews in database</returns>
        public async Task<List<Review>?> GetReviews()
        {

            if (applicationContext?.Reviews == null)
                return null;

            return await applicationContext.Reviews.ToListAsync();

        }

        /// <summary>
        /// Gets specific review.
        /// </summary>
        /// <param name="id">Id of specific review</param>
        /// <returns>Specific review</returns>
        public async Task<Review?> GetReview(int id)
        {

            if (applicationContext?.Reviews == null)
                return null;

            return await applicationContext.Reviews.FirstOrDefaultAsync(t => t.Id == id);

        }

        /// <summary>
        /// Creates new review.
        /// </summary>
        /// <param name="review">The new review to create</param>
        /// <returns>New review that was created</returns>
        public async Task<Review?> CreateReview(Review review)
        {

            if (applicationContext?.Reviews == null)
                return null;

            applicationContext.Reviews?.AddAsync(review);
            await applicationContext.SaveChangesAsync();

            return review;

        }

        /// <summary>
        /// Updates review data.
        /// </summary>
        /// <param name="review">The update review</param>
        /// <returns>The update review</returns>
        public async Task<Review?> UpdateReview(Review review)
        {

            if (applicationContext?.Reviews == null)
                return null;

            applicationContext.Reviews.Update(review);
            await applicationContext.SaveChangesAsync();

            return review;

        }

        /// <summary>
        /// Deletes review from database.
        /// </summary>
        /// <param name="review">The review to delete</param>
        public async void DeleteReview(Review review)
        {

            if (applicationContext?.Reviews == null)
                return;

            applicationContext.Reviews.Remove(review);
            await applicationContext.SaveChangesAsync();

        }

        #endregion

        #region Collection

        /// <summary>
        /// Gets all collections in database.
        /// </summary>
        /// <returns>List of collections in database</returns>
        public async Task<List<Collection>?> GetCollections(string authenticationUserId)
        {

            if (applicationContext?.Collectors == null || applicationContext.Collections == null)
                return null;

            Collector? collector = await applicationContext.Collectors.FirstOrDefaultAsync(c => c.AuthenticationUserId == authenticationUserId);

            if (collector == null)
                return null;

            return await applicationContext.Collections.Where(c => c.OwnerId == collector.Id).ToListAsync();

        }

        /// <summary>
        /// Gets specific collection.
        /// </summary>
        /// <param name="id">Id of specific colection</param>
        /// <returns>Specific collection</returns>
        public async Task<Collection?> GetCollection(int? id, string authenticationUserId)
        {

            if (applicationContext?.Collectors == null || applicationContext.Collections == null || id == null)
                return null;

            Collector? collector = await applicationContext.Collectors.FirstOrDefaultAsync(c => c.AuthenticationUserId == authenticationUserId);

            if (collector == null)
                return null;

            return await applicationContext.Collections.Include(c => c.AnimeFigures).Include(c => c.Collectors).FirstOrDefaultAsync(c => c.Id == id && c.OwnerId == collector.Id);

        }

        /// <summary>
        /// Creates new collection.
        /// </summary>
        /// <param name="collection">The new collection to create</param>
        /// <returns>New collection that was created</returns>
        public async Task<Collection?> CreateCollection(Collection collection)
        {

            if (applicationContext?.Collections == null)
                return null;

            applicationContext.Collections?.AddAsync(collection);
            await applicationContext.SaveChangesAsync();

            return collection;

        }

        /// <summary>
        /// Updates collection data.
        /// </summary>
        /// <param name="collection">The update collection</param>
        /// <returns>The update collection</returns>
        public async Task<Collection?> UpdateCollection(Collection collection)
        {

            if (applicationContext?.Collections == null)
                return null;

            applicationContext.Collections.Update(collection);
            await applicationContext.SaveChangesAsync();

            return collection;

        }

        /// <summary>
        /// Adds new anime figure to collection.
        /// </summary>
        /// <param name="collectionId">Id of collection to add anime figure to</param>
        /// <param name="animeFigure">Anime figure to add to collection</param>
        /// <returns>The collection that got updated</returns>
        public async Task<Collection?> AddAnimeFigureToCollection(int collectionId, AnimeFigure animeFigure)
        {

            if (applicationContext?.Collections == null)
                return null;

            Collection? collection = await applicationContext.Collections.FirstOrDefaultAsync(c => c.Id == collectionId);

            if (collection == null)
                return null;

            collection.AnimeFigures?.Add(animeFigure);
            collection.TotalPrice += animeFigure.Price;
            collection.TotalValue += animeFigure.Value;

            applicationContext.Collections.Update(collection);
            await applicationContext.SaveChangesAsync();

            return collection;

        }

        /// <summary>
        /// Removes anime figure from collection.
        /// </summary>
        /// <param name="collectionId">Id of collection from which the anime figure has to be deleted</param>
        /// <param name="animeFigure">The anime figure to remove from collection</param>
        /// <returns>The collection from which the anime figure was removed</returns>
        public async Task<Collection?> RemoveAnimeFigureFromCollection(int collectionId, AnimeFigure animeFigure)
        {

            if (applicationContext?.Collections == null)
                return null;

            Collection? collection = await applicationContext.Collections.FirstOrDefaultAsync(c => c.Id == collectionId);

            if (collection == null)
                return null;

            collection.AnimeFigures?.Remove(animeFigure);
            collection.TotalPrice -= animeFigure.Price;
            collection.TotalValue -= animeFigure.Value;

            applicationContext.Collections.Update(collection);
            await applicationContext.SaveChangesAsync();

            return collection;

        }

        public async Task<Collection?> ShareCollection(int collectionId, Collector collector)
        {

            if (applicationContext?.Collections == null)
                return null;

            Collection? collection = await applicationContext.Collections.Include(c => c.Collectors).FirstOrDefaultAsync(c => c.Id == collectionId);

            if (collection == null)
                return null;

            collection.Collectors?.Add(collector);

            applicationContext.Collections.Update(collection);
            await applicationContext.SaveChangesAsync();

            return collection;

        }

        /// <summary>
        /// Deletes specific collection from collector.
        /// </summary>
        /// <param name="Id">Id of specific collection to be deleted</param>
        /// <param name="authenticationUserId">User authentication</param>
        /// <returns>Deletion was succesfull</returns>
        public async Task<bool> DeleteCollection(int Id, string authenticationUserId)
        {

            if (applicationContext?.Collections != null && applicationContext.Collectors != null)
            {

                Collector? collector = await applicationContext.Collectors.FirstOrDefaultAsync(c => c.AuthenticationUserId == authenticationUserId);

                if (collector != null)
                {

                    Collection? collection = await applicationContext.Collections.FirstOrDefaultAsync(c => c.Id == Id && c.OwnerId == collector.Id);

                    if (collection != null)
                    {

                        applicationContext.Collections.Remove(collection);
                        await applicationContext.SaveChangesAsync();

                    }
                    else
                        return false;

                }
                else
                    return false;

            }
            else
                return false;

            return true;

        }

        #endregion

    }

}
