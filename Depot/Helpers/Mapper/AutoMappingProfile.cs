using AutoMapper;
using Depot.API.Departments.Requests;
using Depot.API.Departments.Responses;
using Depot.API.Employees.Requests;
using Depot.API.Employees.Responses;
using Depot.API.Entities.Entity.Requests;
using Depot.API.Entities.Entity.Responses;
using Depot.API.Entities.EntityManufactures.Requests;
using Depot.API.Entities.EntityManufactures.Responses;
using Depot.API.Entities.EntityModels.Requests;
using Depot.API.Entities.EntityModels.Responses;
using Depot.API.Entities.EntityTypes.Requests;
using Depot.API.Entities.EntityTypes.Responses;
using Depot.API.Requisitions.RequisitionStatuses.Requests;
using Depot.API.Requisitions.RequisitionStatuses.Responses;
using Depot.API.Transactions.Transaction.Responses;
using Depot.API.Transactions.TransactionTypes.Requests;
using Depot.API.Transactions.TransactionTypes.Responses;
using Depot.API.Users.Requests;
using Depot.API.Users.Responses;
using Depot.Helpers.Mapper.ValueResolvers;
using Depot.Models.Departments;
using Depot.Models.Entities;
using Depot.Models.Requisitions;
using Depot.Models.Transactions;
using Depot.Models.Users;
using Microsoft.AspNetCore.JsonPatch;

namespace Depot.Helpers.Mapper;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<User, UserGetResponse>()
            .ForMember(x => x.Role, opt => opt.MapFrom<UserRoleToResponseResolver>());
        CreateMap<User, UserCreateResponse>()
            .ForMember(x => x.Employee, opt => opt.MapFrom(src => src.Employee));
        CreateMap<User, UserUpdateResponse>();
        CreateMap<UserCreateRequest, User>()
            .ForMember(x => x.Employee, opt => opt.MapFrom(src => src.Employee));
        CreateMap<JsonPatchDocument<UserCreateRequest>, User>();
        CreateMap<JsonPatchDocument<UserPatchRequest>, User>();
        CreateMap<UserPatchRequest, User>();
        
        CreateMap<Employee, EmployeeGetResponse>();
        CreateMap<Employee, EmployeeCreateResponse>();
        CreateMap<Employee, EmployeeUpdateResponse>();
        CreateMap<EmployeeCreateRequest, Employee>();
        CreateMap<EmployeeUpdateRequest, Employee>();
        CreateMap<EmployeeEditMeRequest, Employee>();
        
        CreateMap<Department, DepartmentGetResponse>();
        CreateMap<Department, DepartmentCreateResponse>();
        CreateMap<Department, DepartmentUpdateResponse>();
        CreateMap<DepartmentCreateRequest, Department>();
        CreateMap<DepartmentUpdateRequest, Department>();
        
        CreateMap<EntityType, EntityTypeGetResponse>();
        CreateMap<EntityType, EntityTypeCreateResponse>();
        CreateMap<EntityType, EntityTypeUpdateResponse>();
        CreateMap<EntityTypeCreateRequest, EntityType>();
        CreateMap<EntityTypeUpdateRequest, EntityType>();
        
        CreateMap<EntityManufacture, EntityManufactureGetResponse>();
        CreateMap<EntityManufacture, EntityManufactureCreateResponse>();
        CreateMap<EntityManufacture, EntityManufactureUpdateResponse>();
        CreateMap<EntityManufactureCreateRequest, EntityManufacture>();
        CreateMap<EntityManufactureUpdateRequest, EntityManufacture>();

        CreateMap<EntityModel, EntityModelGetResponse>();
        CreateMap<EntityModel, EntityModelCreateResponse>();
        CreateMap<EntityModel, EntityModelUpdateResponse>();
        CreateMap<EntityModelCreateRequest, EntityModel>();
        CreateMap<EntityModelUpdateRequest, EntityModel>();
        
        CreateMap<Entity, EntityGetResponse>();
        CreateMap<Entity, EntityCreateResponse>();
        CreateMap<Entity, EntityUpdateResponse>();
        CreateMap<EntityCreateRequest, Entity>();
        CreateMap<EntityUpdateRequest, Entity>();
        
        CreateMap<TransactionType, TransactionTypeGetResponse>();
        CreateMap<TransactionType, TransactionTypeCreateResponse>();
        CreateMap<TransactionType, TransactionTypeUpdateResponse>();
        CreateMap<TransactionTypeCreateRequest, TransactionType>();
        CreateMap<TransactionTypeUpdateRequest, TransactionType>();
        
        CreateMap<Transaction, TransactionGetResponse>();
        
        CreateMap<RequisitionStatus, RequisitionStatusGetResponse>();
        CreateMap<RequisitionStatus, RequisitionStatusCreateResponse>();
        CreateMap<RequisitionStatus, RequisitionStatusUpdateResponse>();
        CreateMap<RequisitionStatusCreateRequest, RequisitionStatus>();
        CreateMap<RequisitionStatusUpdateRequest, RequisitionStatus>();
    }
}