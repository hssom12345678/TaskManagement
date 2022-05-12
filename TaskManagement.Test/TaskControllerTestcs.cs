using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using TaskManagement.Controllers;
using TaskManagement.Dtos;
using TaskManagement.Repository.Contracts;
using Xunit;
using TaskManagement.Models;

namespace TaskManagement.Test
{
    public class TaskControllerTestcs
    {
        private readonly Mock<ITaskRepository> repositoryStub = new();
        [Fact]
        public async Task CreateTaskAsync_WithItemToCreate_ReturnsCreatedItem()
        {
            // Arrange
            var taskToCreate = new TaskCreateDTO(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                DateTime.Now.Date,
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date,
                Models.Status.New,
                Priority.Low
                );

            var controller = new TaskController(repositoryStub.Object);

            // Act
            var result = await controller.Post(taskToCreate);

            // Assert
            var createdTask = (result.Result as CreatedAtActionResult).Value as TaskDTO;
            taskToCreate.Should().BeEquivalentTo(
                createdTask,
                options => options.ComparingByMembers<TaskDTO>().ExcludingMissingMembers()
            );
            createdTask.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateItemAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            Tasks existingItem = CreateRandomItem();
            repositoryStub.Setup(repo => repo.GetTaskAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingItem);

            var itemId = existingItem.Id;
            var itemToUpdate = new TaskUpdateDTO(
                Guid.NewGuid().ToString(),
                "Test",
                DateTime.Now,
                DateTime.Now.Date.AddDays(1),
                DateTime.Now,
                Status.New,
                Priority.Low
            );

            var controller = new TaskController(repositoryStub.Object);

            // Act
            var result = await controller.Put(itemId, itemToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
        private Tasks CreateRandomItem()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(1),
                Description = Guid.NewGuid().ToString(),
                Status = Status.New
            };
        }
    }
}
