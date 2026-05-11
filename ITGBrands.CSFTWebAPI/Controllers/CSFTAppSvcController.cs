using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using CSFTWebAPI.Models;
using CSFTWebAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace CSFTWebAPI.Controllers
{
  
    [ApiController]
    [Route("api/")]
    public class CSFTAppSvcController : ControllerBase
    {
        private readonly DataService _dataService;
        private readonly ILogger<CSFTAppSvcController> _logger;

        public CSFTAppSvcController(DataService dataService, ILogger<CSFTAppSvcController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet("get-SaratogaInfo")]
      
        public async Task<IActionResult> GetSaratogaInfo(/*[FromBody] InputModel inputModel*/)
        {
            //if (inputModel == null)
            //{
            //    return BadRequest("Input model is required.");
            //}

            try
            {
                var (tagLayouts, tagDetails, blendCodes, feederMatrices, locations,feederpurge) = await _dataService.GetDataFromTablesAsync();
                var result = new
                {
                    TagLayouts = tagLayouts,
                    TagDetails = tagDetails,
                    BlendCodes = blendCodes,
                    FeederMatrices = feederMatrices,
                    Locations = locations,
                    FeederPurge = feederpurge
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle exceptions and log if needed
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        //[HttpPost("get-DumperLocationDesc")]
        //[Consumes("application/xml")]
        //public async Task<IActionResult> GetDumperLocationDesc([FromBody] InputModel inputModel)
        //{
        //    try
        //    {
        //        var dataSet = await _dataService.GetDataFromTablesAsync(inputModel);

        //        if (dataSet == null || dataSet.Tables.Count == 0)
        //        {
        //            return NotFound("No data found.");
        //        }

        //        // Convert DataSet to a dictionary and serialize to JSON
        //        var dataSetDict = ConvertDataSetToDictionary(dataSet);
        //        var options = new JsonSerializerOptions { WriteIndented = true };
        //        string jsonString = JsonSerializer.Serialize(dataSetDict, options);

        //        return Ok(jsonString);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred in GetDumperLocationDesc");
        //        return StatusCode(500, "Internal server error: " + ex.Message);
        //    }
        //}

        [HttpPost("SendDumpMessage")]
        [Consumes("application/xml")]
        public async Task<IActionResult> SendDumpMessage([FromBody] Msg msg, [FromQuery] string endpoint, [FromQuery] string returnCode, [FromQuery] string returnDescription)
        {
            try
            {
                await _dataService.SaveDumpMessageAsync(msg, endpoint, returnCode, returnDescription);
                return Ok("Dump message inserted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in SendDumpMessage");
                return StatusCode(500, $"Error processing XML data: {ex.Message}");
            }
        }

        [HttpGet("DumpCount")]
        public async Task<IActionResult> GetRecordCount([FromQuery] DateTime recordDate)
        {
            try
            {
                int count = await _dataService.GetRecordCountByDateAsync(recordDate);
                return Ok(new { RecordDate = recordDate, RecordCount = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetRecordCount");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // Method to convert DataSet to Dictionary
        //private Dictionary<string, object> ConvertDataSetToDictionary(DataSet dataSet)
        //{
        //    var result = new Dictionary<string, object>();
        //    foreach (DataTable table in dataSet.Tables)
        //    {
        //        var tableData = new List<Dictionary<string, object>>();
        //        foreach (DataRow row in table.Rows)
        //        {
        //            var rowData = new Dictionary<string, object>();
        //            foreach (DataColumn column in table.Columns)
        //            {
        //                rowData[column.ColumnName] = row[column];
        //            }
        //            tableData.Add(rowData);
        //        }
        //        result[table.TableName] = tableData;
        //    }
        //    return result;
        //}
    }

}