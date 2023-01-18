using Microsoft.EntityFrameworkCore;

namespace MyFinancesWebAPI.Contexts;

public class MyAppContext : DbContext
{
	public MyAppContext(DbContextOptions<MyAppContext> options)
		: base(options){
		
	}
}