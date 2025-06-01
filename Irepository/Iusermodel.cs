using app.models;

namespace app.Irepository
{
    public interface Iusermodel
    {
        public IEnumerable<Usermodel> Getallusers();
        public Usermodel Getuser(int id);
        public void updateuser(int id);
        public void deleteuser(int id);
        public Usermodel adduser(Usermodel user);
    }
}
