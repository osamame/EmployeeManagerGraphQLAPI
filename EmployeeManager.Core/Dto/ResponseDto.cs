using Newtonsoft.Json;

namespace EmployeeManager.Core.Dto
{
    public class ResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
