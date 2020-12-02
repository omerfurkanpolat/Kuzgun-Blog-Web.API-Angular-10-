using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataHandler;
using Sahika.App_Start;
using Sahika.DataAccess.Abstract;
using Sahika.Dtos;
using Sahika.Helper.Filters;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;
using System.Web.Security;

namespace Sahika.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [JwtAuthentication]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IFavouritePostRepository _favouritePostRepository;
        private readonly IPostStatRepository _postStatRepository;
        private readonly IMapper _mapper = AutoMapperConfing._mapper;
        public UsersController(IPostRepository postRepository,
                                ICommentRepository commentRepository,
                                IFavouritePostRepository favouritePostRepository,
                                IPostStatRepository postStatRepository)

        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _favouritePostRepository = favouritePostRepository;
            _postStatRepository = postStatRepository;
        }

        [HttpGet]
        [Route("commentExists/{userId}/{postId}")]
        public IHttpActionResult CommentExists(int userId, int postId)
        {
            var exists = _commentRepository.CommentExists(userId, postId);
            CommentForReturnDTO model = new CommentForReturnDTO();
            model.Exists = exists;

            return Ok(model);
        }

        
        [HttpGet]
        [Route("getComment/{id}")]
        public IHttpActionResult GetComment(int id)
        {
            var postComment = _commentRepository.Get(c => c.PostCommentId == id);
            if (postComment == null)
                return BadRequest("Yorum bulunamadı");
            var result = _mapper.Map<CommentForReturnDTO>(postComment);
            return Ok(result);
        }


        [HttpPost]
        [Route("addcomment/{postId}/{userId}")]
        public IHttpActionResult AddComment( int postId, int userId, CommentForCreationDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest();           


            var result = _commentRepository.CommentExists(userId, postId);
            if (result == true)
                return BadRequest("birden fazla yorum ekleyemezsiniz");

            PostComment postComment = new PostComment();
            postComment.UserId = userId;
            postComment.PostId = postId;
            postComment.Comment = model.Comment;
            postComment.DateCreated = DateTime.Now;

            try
            {
                _commentRepository.AddCommentByUser(postComment);
            }
            catch(Exception)
            {
                throw;
            }
             

            return Ok();
        }




        
        [HttpDelete]
        [Route("deletecomment/{commentId}")]
        public IHttpActionResult DeleteComment(int commentId)
        {
            var postComment = _commentRepository.Get(p => p.PostCommentId == commentId);

            if (postComment == null)
                return BadRequest("böyle bir yorum yok");

            _commentRepository.Delete(postComment);

            return Ok();
        }




        
        [HttpPut]
        [Route("updatecomment/{commentId}/{userId}")]
        public IHttpActionResult UpdateComment( int commentId, int userId, CommentForUpdateDTO model)
        {
          
            if (!ModelState.IsValid)
                return BadRequest("girdiğiniz bilgiler eksik yahut hatalı");

            var postComment = _commentRepository.Get(p => p.PostCommentId == commentId);

            if (postComment == null)
                return BadRequest("böyle bir yorum henüz girilmemiş");
                       

            if (postComment.UserId != userId)
                return Content(HttpStatusCode.Unauthorized, "Bu yorumu güncellemeye yetkiniz yok");


            postComment.Comment = model.comment;
            _commentRepository.Update(postComment);

            return Ok();
        }

      


        [Authorize(Roles = "user,writer,admin")]
        [HttpPost]
        [Route("addfav/{postId}")]
        public IHttpActionResult AddFav(int postId)
        {

            int userId = User.Identity.GetUserId<int>();

            var favToAdd = _favouritePostRepository.AddFavByUser(new FavouritePost { UserId = userId, PostId = postId });

            var postsat = _postStatRepository.Get(s => s.PostStatId == favToAdd.PostId);
            postsat.Claps = postsat.Claps + 1;
            _postStatRepository.Update(postsat);

            return Ok();
        }





        [Authorize(Roles = "user,writer,admin")]
        [HttpDelete]
        [Route("deletefav/{postId}")]
        public IHttpActionResult DeleteFav(int postId)
        {

            int userId = User.Identity.GetUserId<int>();

            var favToDeleted = _favouritePostRepository.GetByPostIdAndUserId(postId, userId);

            if (favToDeleted == null)
                return BadRequest();

            _favouritePostRepository.Delete(favToDeleted);

            var postsat = _postStatRepository.Get(s => s.PostStatId == favToDeleted.PostId);
            postsat.Claps = postsat.Claps - 1;

            _postStatRepository.Update(postsat);

            return Ok();
        }

       

    }
}
