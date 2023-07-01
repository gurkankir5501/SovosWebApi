using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Quartz;
using SovosWebApi.Core.MailServer;
using SovosWebApi.Core.Models;
using SovosWebApi.Core.Repositories;
using SovosWebApi.CustomFilter;
using SovosWebApi.JobSchedulers;
using SovosWebApi.JobSchedulers.Jobs;
using System.Net;

namespace SovosWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceHeaderController : ControllerBase
    {
        private IInvoiceHeaderRepository _invoiceHeaderRepository;
        private IMailer _mailer;
        private IJobScheduler _scheduler;
        private ILogger<InvoiceHeaderController> _logger;
        private IValidator<InvoiceHeader> _validator;

        public InvoiceHeaderController(IInvoiceHeaderRepository invoiceHeaderRepository,
            IMailer mailer,
            IJobScheduler scheduler,
            ILogger<InvoiceHeaderController> logger,
            IValidator<InvoiceHeader> validator
            )
        {
            _invoiceHeaderRepository = invoiceHeaderRepository;
            _mailer = mailer;
            _scheduler = scheduler;
            _logger = logger;
            _validator = validator;
        }

        [HttpPost("[Action]")]
        public IActionResult Create(InvoiceHeader invoiceHeader)
        {
            try
            {
                var validationResult = _validator.Validate(invoiceHeader);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                if (_invoiceHeaderRepository.Get(invoiceHeader.InvoiceId) is not null)
                    throw new Exception("There is a record of the same id !");

                _invoiceHeaderRepository.Add(invoiceHeader);
                IDictionary<string, object> data = new Dictionary<string, object>();
                data.Add(typeof(IMailer).Name, _mailer);
                _scheduler.SchedulerSetup<MailerJob>(new JobDataMap(data));
                _logger.Log(LogLevel.Information, $" A new record with id:{invoiceHeader.InvoiceId} has been created.");
                return Ok(invoiceHeader);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return Problem(e.Message, null, ((int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("[Action]")]
        public IActionResult GetAll()
        {
            try
            {
                _logger.Log(LogLevel.Information, "All records are listed.");
                return Ok(_invoiceHeaderRepository.GetAll());
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return Problem(e.Message, null, ((int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("[Action]/{id}")]
        public IActionResult GetInvoiceAllDetail(string id)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Access to record with id: {id} .");
                return Ok(_invoiceHeaderRepository.GetInvoiceAllDetail(id));
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return Problem(e.Message, null, ((int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
