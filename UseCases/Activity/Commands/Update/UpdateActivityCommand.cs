namespace UseCases.Activity.Commands.Update
{
    using Create;

    /// <summary>
    /// Команда обновление активности
    /// </summary>
    public class UpdateActivityCommand : CreateActivityCommand
    {
        /// <summary>
        /// Идентификатор активности
        /// </summary>
        public Guid Id { get; set; }
    }
}
