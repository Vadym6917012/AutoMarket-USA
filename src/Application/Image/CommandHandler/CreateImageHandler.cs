using Application.Common.Interfaces;
using Application.Image.Commands;
using MediatR;

namespace Application.Image.CommandHandler
{
    public class CreateImageHandler : IRequestHandler<CreateImage>
    {
        private readonly IImagesRepository _imagesRepository;

        public CreateImageHandler(IImagesRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
        }

        public async Task Handle(CreateImage request, CancellationToken cancellationToken)
        {
            var image = new Images
            {
                ImagePath = request.ImagePath,
                ImagePathToDisplay = request.ImagePathToDisplay,
                CarId = request.CarId,
                Car = request.Car,
            };

            await _imagesRepository.AddAsync(image);
        }
    }
}
