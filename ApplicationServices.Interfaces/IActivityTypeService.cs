using Domain.Entities;

namespace ApplicationServices.Interfaces;

public interface IActivityTypeService
{
    IQueryable<ActivityType> GetAll();
}