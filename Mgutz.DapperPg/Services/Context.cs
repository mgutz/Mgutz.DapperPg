namespace Mgutz.DapperPg.Services {

    // <summary>
    // IRequestContet is request scoped HTTP agnostic data. A service should work
    // with any request whether it be from HTTP or gRPC.
    // </summary>
    public class RequestContext {
        public int UserId { get; set; }
    }
}
