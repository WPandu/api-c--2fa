using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace API2FA.Requests
{
    public class MeRequest
    {
        [FromHeader(Name = "Authorization")]
        public string Authorization { get; set; }
    }
}
