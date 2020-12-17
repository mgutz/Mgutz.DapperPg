namespace Mgutz.DapperPg.Services {

    /// <summary>
    /// RequestContext is a request context regardless if it's from an HTTP
    /// or gRPC request.
    /// </summary>
    public class RequestContext {
        public int UserId { get; set; }
    }

}
