using API_Application.Application.DTOs;
using API_Application.Infrastructure.EmployeeRepository;
using AutoMapper;
using MediatR;
using API_Application.Middleware;

namespace API_Application.Application.Queries.GetEmployeeList
{
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeWithoutEmpUuidDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmployeeListQueryHandler> _logger;

        public GetEmployeeListQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper, ILogger<GetEmployeeListQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<EmployeeWithoutEmpUuidDTO>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeeListAsync(request.Limit, request.Offset, request.Search);
                return _mapper.Map<List<EmployeeWithoutEmpUuidDTO>>(employees);
            }
            catch (NoContentException ex)
            {
                _logger.LogWarning(ex, "Employee list is empty !!!.");
                throw new NotFoundException("Employee list is empty.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the GetEmployeeListQuery.");
                throw;
            }
        }
    }
}
