using Nelibur.ObjectMapper;  //Install-Package TinyMapper

//深度到一个新对象
var m = TinyMapper.Map<T>(obj);




//高级操作
TinyMapper.Bind<Person, PersonDto>(config =>
{
	config.Ignore(x => x.Id);
	config.Ignore(x => x.Email);
	config.Bind(source => source.LastName, target => target.Surname);
	config.Bind(target => source.Emails, typeof(List<string>));
});

var person = new Person
{
	Id = Guid.NewGuid(),
	FirstName = "John",
	LastName = "Doe",
	Emails = new List<string>{"support@tinymapper.net", "MyEmail@tinymapper.net"}
};

var personDto = TinyMapper.Map<PersonDto>(person);
