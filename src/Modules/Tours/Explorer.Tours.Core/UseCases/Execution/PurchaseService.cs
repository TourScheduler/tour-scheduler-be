using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository purchaseRepository, ITourRepository tourRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _tourRepository = tourRepository;
            _mapper = mapper;
        }

        public Result<List<PurchaseDto>> Create(string recipientEmail, List<CreatePurchaseDto> purchasesDto)
        {
            try
            {
                List<PurchaseDto> createdPurchases = new List<PurchaseDto>();
                foreach (var p in purchasesDto)
                {
                    if (_tourRepository.GetById((int)p.TourId) != null)
                    {
                        var purchase = _purchaseRepository.Create(new Purchase(p.TouristId, p.TourId, DateTime.Now.ToUniversalTime()));
                        createdPurchases.Add(new PurchaseDto((int)purchase.Id, purchase.TouristId, _mapper.Map<TourDto>(_tourRepository.GetById((int)purchase.TourId)), purchase.PurchaseDate));
                    }
                    else
                    {
                        return Result.Fail(FailureCode.InvalidArgument);
                    }
                }

                SendConfirmationEmail(recipientEmail, createdPurchases);

                return createdPurchases;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                // There is a subtle issue here. Can you find it?
            }
        }

        private static void SendConfirmationEmail(string receiverEmail, List<PurchaseDto> createdPurchases)
        {
            string senderEmail = "isaprojekat1@gmail.com";
            string senderPassword = "bpyjrqpswdubdjrf";

            string recipientEmail = receiverEmail;

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                var message = new MailMessage(senderEmail, recipientEmail);
                message.Subject = "Tours";
                message.Body = "Purchased tours: " + string.Join(", ", createdPurchases.Select(p => p.Tour.Name));

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
        }
    }
}
