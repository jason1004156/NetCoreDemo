using Microsoft.AspNetCore.Mvc;
using Demo.Service;
using Demo.Common;

namespace Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class SecurtiesController : ControllerBase
{

    private readonly ILogger<SecurtiesController> _logger;
    private readonly ISecurtiesService _securtiesService;
    //private readonly DemoContext _db;

    public SecurtiesController(ILogger<SecurtiesController> logger, ISecurtiesService securtiesService
        //, DemoContext db
        )
    {
        _logger = logger;
        _securtiesService = securtiesService;
        //_db = db;
    }

    [HttpPost("RefreshData")]
    public async Task<ApiResponse> RefreshData()
    {
        var res = await _securtiesService.RefreshData();
        return ApiResponse.Instance.CreateOK(res);
    }

    [HttpGet("GetSceurities")]
    public async Task<ApiResponse> GetSecurities()
    {
        var res = await _securtiesService.GetSecurity();
        return ApiResponse.Instance.CreateOK(res);
    }


}

