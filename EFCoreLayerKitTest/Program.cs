using EFCoreLayerKit.Core;
using EFCoreLayerKitTest.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreLayerKitTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 配置 DI
            var services = new ServiceCollection();
            services.AddEFCoreLayerKit();

            // 获取服务
            var provider = services.BuildServiceProvider();
            var authorRepo = provider.GetRequiredService<AuthorRepository>();
            var bookRepo = provider.GetRequiredService<BookRepository>();

            // 增加数据
            var author = new Entities.Author { Name = "张三" };
            var addAuthorResult = await authorRepo.AddAsync(author);
            Console.WriteLine($"添加作者: {addAuthorResult.Data?.Name}, ID: {addAuthorResult.Data?.Id}");
            Console.WriteLine($"添加结果: {addAuthorResult}");

            var book = new Entities.Book { Title = "C# 入门", AuthorId = author.Id };
            var addBookResult = await bookRepo.AddAsync(book);
            Console.WriteLine($"添加书籍: {addBookResult.Data?.Title}, ID: {addBookResult.Data?.Id}");
            Console.WriteLine($"添加结果: {addBookResult}");

            Console.WriteLine();

            // 查询
            var authors = await authorRepo.GetAllAsync();
            Console.WriteLine($"作者数量: {authors.Data?.Count}");

            // 修改
            author.Name = "李四";
            var updateAuthorResult = await authorRepo.UpdateAsync(author);
            Console.WriteLine($"更新作者: {updateAuthorResult.Data?.Name}, ID: {updateAuthorResult.Data?.Id}");
            Console.WriteLine($"更新结果: {updateAuthorResult}");

            // 删除
            var deleteAuthorResult = await authorRepo.DeleteAsync(author.Id);
            Console.WriteLine($"删除结果: {deleteAuthorResult}");

            Console.WriteLine("测试完成");
        }
    }
}