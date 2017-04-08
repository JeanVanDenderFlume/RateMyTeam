namespace RateMyTeam.Models.Abstract
{
    public interface IStudent
    {
        IQueryable<Student> Students { get; }

        void Save(Student student)

        ....
    }
}