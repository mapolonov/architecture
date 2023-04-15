// right ways to call async from sync methods
public string DoSync()
{
	return DoAsync().Result; //blocking
	return DoAsync().GetAwaiter().GetResult(); // better, but depends on context
	return Task.Run(() => DoAsync()).Result; //AggregatedException
	return Task.Run(() => DoAsync()).GetAwaiter().GetResult(); //first Exception with Aggregated prop
	return Task.Run(async () => await DoAsync()).GetAwaiter().GetResult(); //twice wrapped
}

//Inject async dependency to some service
class Service : IService
{
	private readonly SomeData _data;
	public SomeData Data => _data;
	private Service(SomeData data) => _data = data;
	
	public static async Task<Service> CreateAsync(IAsyncdependency asyncDependency)
	{
		var data = await asyncDependency.DoAsync();
		return new Service(data);
	}
}
OR
class Service : IService
{
	private SomeData _data; // is not readonly anymore here
	public SomeData Data => _data;
	private Service() {}
	
	private async Task<Service> InitAsync(IAsyncdependency asyncDependency)
	{
		_ data = await asyncDependency.DoAsync();
		return this;
	}
	
	public static Task<Service> CreateAsync(IAsyncdependency asyncDependency)
	{
		var service = new Service();
		return service.InitAsync(asyncDependency);
	}
}

var service = await Service.CreateAsync(asyncDependency);
