using AutoMapper;
using Sahika.App_Start;
using Sahika.DataAccess.Abstract;
using Sahika.Dtos;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Sahika.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [JwtAuthentication]
    [RoutePrefix("api/writers")]
    public class WritersController : ApiController
    {
        private readonly IPostRepository _postRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
       

        private readonly IMapper _mapper = AutoMapperConfing._mapper;
        public WritersController( IPostRepository postRepository,
            ISubCategoryRepository subCategoryRepository
           )

        {
            _postRepository = postRepository;
            _subCategoryRepository = subCategoryRepository;
            
        }

        [HttpGet]
        [Route("getSubcategories")]
        public IHttpActionResult GetSubCategory()
        {
            var subcategories = _subCategoryRepository.GetList();
            var result = _mapper.Map<IEnumerable<SubCategoryForReturnDTO>>(subcategories);
            return Ok(result);
        }



        [HttpPost]
        [Route("addPost/{userId}")]
        public IHttpActionResult AddPost(int userId, PostForCreationDTO model)
        {


            if (!ModelState.IsValid)
                return BadRequest("girdiğiniz bilgiler hatalı");


            Post post = new Post
            {
                Body = model.Body,
                Title = model.Title,
                UserId = userId,
                ImageUrl = model.ImageUrl,
                DateCreated = DateTime.UtcNow,
                CategoryId=model.CategoryId,
                SubCategoryId=model.SubCategoryId

            };

            try
            {
                _postRepository.AddPostWithStatsAndCategory(post, model.SubCategoryId);

            }
            catch (Exception)
            {

                throw;
            }


            return Ok();
        }



        [HttpPut]
        
        [Route("updatePost/{role}")]
        public IHttpActionResult UpdatePost(string role, PostForUpdateDTO model)
        {

            if (!ModelState.IsValid || model == null)
                return BadRequest("Eksik yahut hatalı bilgi girdiniz");

            var post = _postRepository.Get(p => p.PostId == model.PostId);
            if (post == null)
                return BadRequest("böyle bir post mevcut değil");


            if (post.UserId != model.UserId && role != "admin")
            {
                return BadRequest("Bu Makaleyi Değiştirmeye Yetkiniz Yok");
            }

            post.Title = model.Title;
            post.Body = model.Body;
            post.ImageUrl = model.ImageUrl;
            post.SubCategoryId = model.SubCategoryId;
            post.CategoryId = model.CategoryId;
            

            

            _postRepository.Update(post);

            return Ok();

        }






        [HttpDelete]
        [Route("deletepost/{postId}")]
        public IHttpActionResult DeletePost([FromUri] int postId)
        {

            var post = _postRepository.Get(p => p.PostId == postId);

            if (post == null)
                return BadRequest("silmeye çalıştığınız kayıt mevcut değil");


            _postRepository.DeletePostWithRelatedEntites(postId);

            return Ok("kayıt silindi");

        }
    }

}
