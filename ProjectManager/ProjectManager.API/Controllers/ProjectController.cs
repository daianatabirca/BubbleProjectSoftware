using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using ProjectManager.Services;
using System.Text.Json;

namespace ProjectManager.Controllers
{
    [ApiController]
    [Route("/api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectManagerRepository _projectManagerRepository;
        private readonly IMapper _mapper;

        public ProjectController(IProjectManagerRepository projectManagerRepository, IMapper mapper)
        {
            _projectManagerRepository = projectManagerRepository ?? throw new ArgumentNullException(nameof(projectManagerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {

            //From Query label with name variable responsible for filter (? bc. it can be nullable)
            var projectEntity = await _projectManagerRepository.GetProjectsAsync();

            return Ok(_mapper.Map<IEnumerable<ProjectDto>>(projectEntity)); //from profile
        }
    }
}
