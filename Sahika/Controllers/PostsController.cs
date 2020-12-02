

using AutoMapper;

using Sahika.App_Start;
using Sahika.DataAccess.Abstract;
using Sahika.Dtos;
using Sahika.Helper.Filters;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace Sahika.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [JwtAuthentication]
    [RoutePrefix("api/{posts}")]
    public class PostsController : ApiController
    {
        private readonly IPostRepository _postRepository;
        private readonly ISubCategoryRepository _subCategory;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper = AutoMapperConfing._mapper;



        public PostsController(IPostRepository postRepository,
                                 ISubCategoryRepository subCategoryRepository,
                                 ICategoryRepository categoryRepository,
                                 ICommentRepository commentRepository
                                 )
        {
            _postRepository = postRepository;
            _subCategory = subCategoryRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;



        }



        [HttpGet]
        [Route("getLastPost")]
        public IHttpActionResult LastPost()
        {
            var post = _postRepository.GetLastPost();
            if (post == null)
            { return BadRequest("böyle bir kayıt mevcut değil"); }
            var result = _mapper.Map<PostForReturnDTO>(post);
            return Ok(result);
        }

        [HttpGet]
        [Route("getAllPost")]
        public IHttpActionResult GetAllPost()
        {
            var posts = _postRepository.GetAll();
            var result = _mapper.Map<IEnumerable<PostForReturnDTO>>(posts);
            return Ok(result);
        }


        [HttpGet]
        [Route("getPostsBySubCategory/{subCategoryId}")]
        public IHttpActionResult GetPostsBySubCategory(int subCategoryId)
        {

            var posts = _postRepository.GetPostBySubCategory(subCategoryId);
            if (posts == null)
                return BadRequest("böyle bir kayıt mevcut değil");
            var result = _mapper.Map<List<PostForReturnDTO>>(posts);
            return Ok(result);
        }




        [HttpGet]
        [Route("getPostsWithEverything/{postId}")]
        public IHttpActionResult GetPostWithEverything(int postId)
        {
            var post = _postRepository.GetPostWithEverything(postId);
            if (post == null)
            { return BadRequest("böyle bir kayıt mevcut değil"); }

            var result = _mapper.Map<PostForReturnDTO>(post);
            return Ok(result);
        }





        [HttpGet]
        [Route("getpostbyuser/{userId}")]
        public IHttpActionResult GetPostByUser(int userId)
        {
            var posts = _postRepository.GetPostsByUser(userId);
            var result = _mapper.Map<IEnumerable<PostForReturnDTO>>(posts);
            return Ok(result);
        }

        [HttpGet]
        [Route("getPostByUserId/{userId}/{postId}")]
        public IHttpActionResult GetPostByUserId(int userId, int postId)
        {
            var post = _postRepository.GetPostByUserId(userId, postId);
            var result = _mapper.Map<PostForReturnDTO>(post);
            return Ok(result);
        }



        [HttpGet]
        [Route("getpopularpost")]
        public IHttpActionResult GetPopularPost()
        {
            var post = _postRepository.GetPostByView();
            return Ok(post);
        }




        [HttpGet]
        [Route("getPostsByCategory/{categoryId}")]
        public IHttpActionResult GetPostsByCategory(int categoryId)
        {
            var posts = _postRepository.GetPostsByCategory(categoryId);
            if (posts == null) return BadRequest("böyle bir kayıt mevcut değil");
            var result = _mapper.Map<List<PostForReturnDTO>>(posts);
            return Ok(result);
        }




        [HttpGet]
        [Route("getpostsWithinTwoDates/{start}/{end}")]
        public IHttpActionResult GetPostsByCategory(DateTime start, DateTime end)
        {
            var posts = _postRepository.GetPostsByTwoDates(start, end);
            if (posts == null) return BadRequest("böyle bir kayıt mevcut değil");
            return Ok(posts);
        }




        
        [HttpGet]
        [Route("GetPostsByDateCreated/{count}")]
        public IHttpActionResult GetPostsByDateCreated(int count)
        {
            var posts = _postRepository.GetPostsByDateCreated(count);
            if (posts == null) return BadRequest("böyle bir kayıt mevcut değil");
            return Ok(posts);
        }



        [HttpGet]
        [Route("GetMostLikedPost")]
        public IHttpActionResult GetMostLikedPost()
        {
            var post = _postRepository.GetMostLikedPost();
            return Ok(post);
        }


        
       
        [HttpPut]
        [Route("deneme/{Id}")]

        public IHttpActionResult Deneme ([FromUri]int Id,[FromBody]SubCategory model)
        {
            if (!ModelState.IsValid) return BadRequest("Girmiş olduğunuz bilgiler eksik ve ya hatalı");         
            if (_categoryRepository.CategoryExists(model.CategoryId)) return BadRequest("Hata Kategori id");
            _subCategory.Update(model);
            return Ok();
            
        }


        [HttpGet]
        [Route("GetCommentByPostId/{postId}")]
        public IHttpActionResult GetCommentByPostId(int postId)
        {
            
            var comments = _commentRepository.GetCommentsByPost(postId);
            if(comments==null)
            {
                return BadRequest("Yorum Bulunamadı");
            }
            var result = _mapper.Map<List<CommentForReturnDTO>>(comments);
           
            
           
            return Ok(result);
        }


        [HttpGet]
        [Route("getCategoriesLastPost")]
        public IHttpActionResult GetCategoryLastPost()
        {
            List<Post> posts = new List<Post>();
            var categoryIds = _categoryRepository.GetCategoryIds();
            foreach(int categoryId in categoryIds)
            {
                Post post = _postRepository.GetLastPostOfCategory(categoryId);
                if(post==null)
                {
                    continue;
                }
                posts.Add(post);
              
            }
            var result = _mapper.Map<List<PostForReturnDTO>>(posts);
           
            return Ok(result);
           
        }
        //For Admin
        [HttpGet]
        [Route("getPost/{postId}")]
        public IHttpActionResult GetPost(int postId)
        {
            var post = _postRepository.GetPost(postId);
            var result = _mapper.Map<PostForReturnDTO>(post);
            return Ok(result);
        }

      

    }
}
