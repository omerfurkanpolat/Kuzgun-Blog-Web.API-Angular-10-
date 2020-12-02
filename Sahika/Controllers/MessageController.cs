using AutoMapper;
using Microsoft.AspNet.Identity;
using Sahika.App_Start;
using Sahika.DataAccess.Abstract;
using Sahika.Dtos;
using Sahika.Helper.Filters;
using Sahika.Helper.Services;
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
    [RoutePrefix("api/message")]   
    
    public class MessageController : ApiController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper = AutoMapperConfing._mapper;
        public MessageController(IMessageRepository messageRepository, IEmailService emailService)
        {
            _messageRepository = messageRepository;
            _emailService = emailService;
            
        }

        [HttpPost]
        [Route("SendMessage")]
        public IHttpActionResult SendMessage(MessageForCreationDTO model)
        {            
            
            if (ModelState.IsValid)
            {
                var message = new Message
                {
                    
                    MessageHeader=model.MessageHeader,
                    MessageBody = model.MessageBody,
                    Email=model.Email,
                    FullName=model.FullName
                    
                };
                _messageRepository.Add(message);
                _emailService.SendMail(model.FullName, model.Email, model.MessageHeader, $"{model.MessageBody} hakkında attığınız mesaj bize ulaştı.\n\nMesajınızı değerlendirilerek en kısa sürede size geri dönüş yapacaktır." );
                return Ok("Messajınız gönderildi");
               
            }
            return BadRequest("Eksik bilgi doldurdunuz");
            
        }
        [HttpPost]
        [Route("AnswerMessage")]
        public IHttpActionResult AnswerMessage(MessageForCreationDTO model)
        {

            if (ModelState.IsValid)
            {
                var message = new Message
                {

                    MessageHeader = model.MessageHeader,
                    MessageBody = model.Answer,
                    Email = model.Email,
                    FullName = model.FullName

                };
                _messageRepository.Add(message);
                _emailService.SendMail(model.FullName, model.Email, model.MessageHeader, $"{model.Answer} ");
                return Ok("Messajınız gönderildi");

            }
            return BadRequest("Eksik bilgi doldurdunuz");

        }

        [HttpGet]
        [Route("GetMessages")]
        public IHttpActionResult GetMessages()
        {
            var getMessages = _messageRepository.GetList();
            if(getMessages!=null)
            {
                return Ok(getMessages);
            }
            return BadRequest();
            

        }

        [HttpGet]
        [Route("GetMessage/{id}")]
        public IHttpActionResult GetMessage(int id)
        {
            var message = _messageRepository.Get(c=>c.MessageId == id);
            if(message!=null)
            {
                return Ok(message);
            }
            return BadRequest("Messaj Bulunamadı");
        }
    }
}
