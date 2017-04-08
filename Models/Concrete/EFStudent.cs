public EFStudent() : IStudent {

    private readonly EFDbContext _context;

    public EFStudent() {
        _context = new EFDbContext()
    }

    public EFStudent(EFDbContext context) {
        _context = context;
    }

    //Assign the password in the controller, and pass the whole object in, after doing a Model.IsValid first
    public static void Save(Student student) {
            callUpdateGraph(student);
            _context.SaveChanges();
}

    protected void CallUpdateGraph(Student student)
    {
        this._context.UpdateGraph(
                student,
                map =>
                map.AssociatedEntity(m => ..)
                    .AssociatedEntity(m => ..)
                    .OwnedCollection(m => .., amap => amap.AssociatedEntity(..)));
    }
}