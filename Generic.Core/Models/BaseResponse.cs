using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic.Core.Models
{
    public class BaseResponse<T>
    {
        public T DataResponse { get; set; }
        public List<DetailResponse> Details { get; set; }
        public bool Successful { get; set; }
        public DetailResponse AddDetailResponse(int Id, String Message) {
            DetailResponse err = new DetailResponse();
            err.Id = Id;
            err.Message = Message;
            return err;
        }
        public List<string> errors { get; set; }
    }
    public class DetailResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }        
    }
    
    
}
