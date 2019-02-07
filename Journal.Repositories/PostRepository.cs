using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Journal.Configurations;
using Journal.Entities;
using Journal.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace Journal.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        #region Get Connection
        public PostRepository(IOptionsMonitor<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.CurrentValue;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionStrings.DefaultConnection);
            }
        }
        #endregion

        #region Stored Procedures
        const string sp_Post_GetAll = "Sp_Post_GetAll";
        const string sp_Post_GetByAuthor = "Sp_Post_GetByAuthor";
        const string sp_Post_GetById = "Sp_Post_GetById";
        const string sp_Post_GetByTag = "Sp_Post_GetByTag";
        const string sp_Post_InsertPost = "Sp_Post_InsertPost";
        const string sp_Post_UpdatePost = "Sp_Post_UpdatePost";
        const string sp_Post_DeletePost = "Sp_Post_DeletePost";
        #endregion
        public void DeletePost(Post post)
        {
            using (IDbConnection dbConnection = Connection)
            {
                try
                {
                    dbConnection.Open();
                    using (var transaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            dbConnection.Query<Post>(sp_Post_DeletePost,
                                new
                                {
                                    PostId = post.PostId,
                                    UpdatedBy = post.UpdatedBy
                                },
                                commandType: CommandType.StoredProcedure);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }


                }
                catch (Exception ex)
                {
                    //placeholder
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }

            }
        }

        public List<Post> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                List<Post> posts = new List<Post>();
                try
                {
                    dbConnection.Open();
                    Dictionary<long, Post> postDictionary = new Dictionary<long, Post>();
                    posts = dbConnection.Query<Post, User, Tag, Post>(sp_Post_GetAll,
                        (post, user, tag) => 
                            { 
                                Post postEntry;
                                post.Author = user; 
                                
                                if (!postDictionary.TryGetValue(post.PostId, out postEntry))
                                {
                                    postEntry = post;
                                    postEntry.Tags = new List<Tag>();
                                    postDictionary.Add(postEntry.PostId, postEntry);
                                }

                                postEntry.Tags.Add(tag);
                                return post;
                            },
                        splitOn:"Username, TagId",
                        commandType: CommandType.StoredProcedure).AsList();
                }
                catch (Exception ex)
                {
                    //placeholder
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }

                return posts;
            }
        }

        public List<Post> GetByAuthor(long userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                List<Post> posts = null;
                try
                {
                    dbConnection.Open();
                    Dictionary<long, Post> postDictionary = new Dictionary<long, Post>();
                    posts = dbConnection.Query<Post, User, Tag, Post>(sp_Post_GetByAuthor,
                        (post, user, tag) => 
                            { 
                                Post postEntry;
                                post.Author = user; 
                                
                                if (!postDictionary.TryGetValue(post.PostId, out postEntry))
                                {
                                    postEntry = post;
                                    postEntry.Tags = new List<Tag>();
                                    postDictionary.Add(postEntry.PostId, postEntry);
                                }

                                postEntry.Tags.Add(tag);
                                return post;
                            },
                        new { UserId = userId },
                        splitOn:"Username, TagId",
                        commandType: CommandType.StoredProcedure).AsList();

                }
                catch (Exception ex)
                {
                    //placeholder
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }

                return posts;
            }
        }

        public Post GetById(long postId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                Post post = null;
                try
                {
                    dbConnection.Open();
                    Dictionary<long, Post> postDictionary = new Dictionary<long, Post>();
                    post = dbConnection.Query<Post, User, Tag, Post>(sp_Post_GetById,
                        (currentPost, user, tag) => 
                            { 
                                Post postEntry;
                                currentPost.Author = user; 
                                
                                if (!postDictionary.TryGetValue(currentPost.PostId, out postEntry))
                                {
                                    postEntry = currentPost;
                                    postEntry.Tags = new List<Tag>();
                                    postDictionary.Add(postEntry.PostId, postEntry);
                                }

                                postEntry.Tags.Add(tag);
                                return currentPost;
                            },
                        new { PostId = postId },
                        splitOn:"Username, TagId",
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                }
                catch (Exception ex)
                {
                    //placeholder
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }

                return post;
            }
        }

        public List<Post> GetByTag(string tagId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                List<Post> posts = null;
                try
                {
                    dbConnection.Open();
                    Dictionary<long, Post> postDictionary = new Dictionary<long, Post>();
                    posts = dbConnection.Query<Post, User, Tag, Post>(sp_Post_GetByTag,
                        (post, user, tag) => 
                            { 
                                Post postEntry;
                                post.Author = user; 
                                
                                if (!postDictionary.TryGetValue(post.PostId, out postEntry))
                                {
                                    postEntry = post;
                                    postEntry.Tags = new List<Tag>();
                                    postDictionary.Add(postEntry.PostId, postEntry);
                                }

                                postEntry.Tags.Add(tag);
                                return post;
                            },
                        new { TagId = tagId },
                        splitOn:"Username, TagId",
                        commandType: CommandType.StoredProcedure).AsList();

                }
                catch (Exception ex)
                {
                    //placeholder
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }

                return posts;
            }
        }

        public void InsertPost(Post post)
        {
            using (IDbConnection dbConnection = Connection)
            {
                List<Post> posts = new List<Post>();
                try
                {
                    dbConnection.Open();
                    using (var transaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            dbConnection.Query<Post>(sp_Post_InsertPost,
                            new
                            {
                                Title = post.Title,
                                Body = post.Body,
                                MoodPercent = post.MoodPercent,
                                UserId = post.UserId,
                                CreatedBy = post.CreatedBy,
                                UpdatedBy = post.UpdatedBy
                            },
                                transaction: transaction,
                                commandType: CommandType.StoredProcedure);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }

                }
                catch (Exception ex)
                {
                    //placeholder
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        public void UpdatePost(Post post)
        {
            using (IDbConnection dbConnection = Connection)
            {
                List<Post> posts = new List<Post>();
                try
                {
                    dbConnection.Open();
                    using (var transaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            dbConnection.Query<Post>(sp_Post_UpdatePost,
                            new
                            {
                                PostId = post.PostId,
                                Title = post.Title,
                                Body = post.Body,
                                MoodPercent = post.MoodPercent,
                                UpdatedBy = post.UpdatedBy
                            },
                                transaction: transaction,
                                commandType: CommandType.StoredProcedure);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }

                }
                catch (Exception ex)
                {
                    //placeholder
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
    }
}