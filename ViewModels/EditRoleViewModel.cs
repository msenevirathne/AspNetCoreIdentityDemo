using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityDemo.ViewModels
{
    public class EditRoleViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        public string RoleName { get; set; }
    }
}
