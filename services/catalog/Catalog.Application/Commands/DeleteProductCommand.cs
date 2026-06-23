using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public string Id { get; set; } = default!;
        public DeleteProductCommand(string id) => Id = id;
    }
}
