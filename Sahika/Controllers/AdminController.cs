using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using static Sahika.App_Start.IdentityConfig;
using static Sahika.Models.IdentityModels;

namespace Sahika.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [JwtAuthentication]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper = AutoMapperConfing._mapper;
        public AdminController(ICategoryRepository categoryRepository,
                                ISubCategoryRepository subCategoryRepository,
                                ICommentRepository commentRepository
                                )
        {
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _commentRepository = commentRepository;
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [Route("getCategories")]
        public IHttpActionResult GetCategories()
        {
            
            var categories = _categoryRepository.GetAll();
            var result = _mapper.Map<IEnumerable<CategoryForReturnDTO>>(categories);
            if(categories!=null)
            {                
                return Ok(result);
            }
            return BadRequest("Kategoriler Getirilemedi");
                
        }

        [HttpPost]
        [Route("createCategory")]
        public IHttpActionResult CreateCategory(CategoryForCreationDTO model)
        {
            var category = new Category
            {
                CategoryName = model.CategoryName,
             

            };
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                return Ok();
            }
            return BadRequest();       
                   
        }

        [HttpGet]
        [Route("getCategory/{id}")]
        public IHttpActionResult GetCategory( int id)
        {
            
            var category = _categoryRepository.GetCategoryWithSubs(id);
            if(category==null)
            {
                return BadRequest("Kategori bulunamadı");
            }
            var result = _mapper.Map<CategoryForReturnDTO>(category);
           
            return Ok(category);
            
        }


        [HttpPut]
        [Route("UpdateCategory/{id}")]
        public IHttpActionResult UpdateCategory(CategoryForUpdateDTO model , int id)
        {
            var category = _categoryRepository.Get(c => c.CategoryId == id);
            
            category.CategoryName = model.CategoryName;
            
            

            if(ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                return Ok();
            }
            return BadRequest("Kategori Güncellenemedi");
                                  

        }

        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public IHttpActionResult DeleteCategory( int id)
        {
            var category = _categoryRepository.Get(c => c.CategoryId == id);
            if(category!=null)
            {
                _categoryRepository.Delete(category);
                return Ok("Kategori Silindi");
            };
                        
            return BadRequest("Kategori Bulunamadı");

        }

        [HttpGet]
        [Route("GetCategories/{id}/GetSubCategories")]
        public IHttpActionResult GetSubCategories(int id)
        {

            var subcategories = _subCategoryRepository.GetAllByCategoryId(id);
            if (subcategories != null)
            {
                var result = _mapper.Map<IEnumerable<SubCategoryForReturnDTO>>(subcategories);
                return Ok(result);
            };
            return BadRequest("Alt kategori bulunamadı");

        }


        [HttpPost]
        [Route("GetCategories/{id}/CreateSubCategory")]
        public IHttpActionResult CreateSubCategory(SubCategoryForCreationDTO model,int id)
        {
            model.CategoryId= id;
            var subCategory = new SubCategory
            {
                SubCategoryName = model.SubCategoryName,
                CategoryId=model.CategoryId

            };
            if(ModelState.IsValid)
            {
                _subCategoryRepository.Add(subCategory);
                return Ok();
            }
            return BadRequest(ModelState);
           
        }
        
        [HttpGet]
        [Route("GetSubCategory/{id}")]
        public IHttpActionResult GetSubCategory( int id)
        {
            var subCategory = _subCategoryRepository.Get(s => s.SubCategoryId == id);
            if (subCategory == null)
            {
                return BadRequest("Kategori bulunamadı");
            }
            var result = _mapper.Map<SubCategoryForReturnDTO>(subCategory);     
            return Ok(result);

        }

        [HttpPut]
        [Route("UpdateSubCategory/{id}")]
        public IHttpActionResult UpdateSubCategory(SubCategoryForUpdateDTO model, int id)
        {
            var subCategory = _subCategoryRepository.Get(s => s.SubCategoryId == id);
            if (subCategory == null)
            {
                return BadRequest("Alt kategori bulunamadı");
            }
            subCategory.SubCategoryName = model.SubCategoryName;
            if(ModelState.IsValid)
            {
                _subCategoryRepository.UpdateSubCategory(subCategory);
                return Ok();
            }
            return BadRequest("Alt kategori güncellenemedi");

        }

        [HttpDelete]
        [Route("DeleteSubCategory/{id}")]
        public IHttpActionResult DeleteSubCategory(int id)
        {
            var subCategory = _subCategoryRepository.Get(c => c.SubCategoryId == id);
            if (subCategory != null)
            {
                _subCategoryRepository.Delete(subCategory);
                return Ok("Kategori Silindi");
            };

            return BadRequest("Kategori Bulunamadı");

        }


        [HttpGet]
        [Route("getroles")]
        public IHttpActionResult GetRoles()
        {
            var roles = RoleManager.Roles.ToList();


            if (roles == null)
                return BadRequest("Roller Bulunamadı");

            return Ok(roles);
        }




        [HttpPost]
        [Route("addrole")]
        public async Task<IHttpActionResult> AddRole(RoleForCreationDTO model)
        {
            if (ModelState.IsValid)
            {

                var role = new ApplicationRole(model.Name.ToLower());
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return BadRequest();
                }
                return Ok();
            }
            return BadRequest("Rol oluşturulamadı");
        }

        [HttpPut]
        [Route("updaterole/{id}")]

        public async Task<IHttpActionResult> Edit(int id, RoleForUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(id);
                role.Name = model.Name;
                await RoleManager.UpdateAsync(role);
                return Ok("rol başarıyla güncellendi");
            }
            return BadRequest("Eksisk veya hatalı bilgi girdiniz");
        }


        [HttpDelete]
        [Route("deleterole/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return BadRequest("Rol Bulunamadı");
                }

                var result = await RoleManager.DeleteAsync(role);

                return Ok();
            }
            return BadRequest("Rol Bilgisi Gelmedi");

        }
        [HttpPut]
        [Route("changeUserRole/{userId}")]
        public IHttpActionResult ChangeUserRole(int userId, UserForChangeRoleDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Rol ismi alınamadı");

            var userRole = UserManager.GetRoles(userId).First();
            try
            {
                UserManager.RemoveFromRole(userId, userRole);
                UserManager.AddToRole(userId, model.name);
            }
            catch (Exception)
            {

                throw;
            }

            return Ok();
        }

        [HttpGet]
        [Route("getUsers")]
        public IHttpActionResult GetUserList()
        {
            var users = UserManager.Users.ToList();
            if (users == null)
            {
                return BadRequest("Kullanıcılar Getirilemedi");
            }
            var result = _mapper.Map<IEnumerable<UserForDetailDTO>>(users);
            return Ok(result);
        }

        [HttpGet]
        [Route("getUserDetail/{id}")]
        public async Task<IHttpActionResult> Details(int id)
        {
            if (id > 0)
            {
                var user = UserManager.FindById(id);
                var userRole = await UserManager.GetRolesAsync(user.Id);
                var result = _mapper.Map<UserForDetailDTO>(user);
                result.UserRole = userRole.FirstOrDefault();

                return Ok(result);
            }

            return BadRequest("Kullanıcı Detayları getirilemedi");
        }



        [HttpDelete]
        [Route("deleteUser/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            if (id > 0)
            {
                var user = UserManager.FindById(id);
                if (user == null)
                {
                    return BadRequest("Kullanıcı Bulunamadı");
                }
                user.IsDeleted = true;
                UserManager.Update(user);
                return Ok();
            }

            return BadRequest();

        }



    }
}
