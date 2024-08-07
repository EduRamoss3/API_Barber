﻿using Barber.Application.CQRS.Schedule.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class UpdateValueForScheduleCommandHandler : IRequestHandler<UpdateValueForScheduleCommand, Schedules>
    {
        private readonly IUnityOfWork _uof; 
        public UpdateValueForScheduleCommandHandler(IUnityOfWork uof)
        {
            _uof = uof;
        }
        public async Task<Schedules> Handle(UpdateValueForScheduleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Request is null");
            }
            var schedule = await _uof.SchedulesRepository.GetByIdAsync(p => p.Id == request.Id);
         
            schedule.UpdateValueForService(request.ValueForService);
            _uof.SchedulesRepository.UpdateValueFor(schedule);
            await _uof.Commit();
            return schedule;
        }
    }
}
