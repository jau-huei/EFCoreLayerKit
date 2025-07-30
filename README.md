# EFCoreLayerKit

EFCoreLayerKit ��һ������ Entity Framework Core ��ͨ�����ݷ��ʲ㹤�߰���֧���Զ��ִ���DTO ӳ�䡢��ɾ�����Զ�Ǩ�Ƶȹ��ܣ������� .NET 8+ ��Ŀ��

## ��Ҫ����
- �Զ�ע�����м̳��� `BaseDbContext` �������ģ����Զ�Ǩ�����ݿ�ṹ
- �Զ�ע�����м̳��� `BaseRepository<TEntity>` �Ĳִ���
- ֧����ɾ�����ֹ��������������ȳ������ݷ���ģʽ
- ֧�� DTO ��ʵ����Զ�˫��ӳ�䣨���� AutoMapper��
- ������ Blazor��ASP.NET Core ������ע�볡��

## ���ٿ�ʼ

1. **��װ����**

ȷ�������Ŀ������ EntityFrameworkCore��AutoMapper �����⡣

2. **���� DbContext �� Repository**
public class MyDbContext : BaseDbContext { /* ... */ }
public class MyEntityRepository : BaseRepository<MyEntity> { public MyEntityRepository(MyDbContext ctx) : base(ctx) { } }
3. **�ڳ�������ʱע�����**
// �� Program.cs �� Startup.cs
services.AddEFCoreLayerKit();
4. **�Զ�Ǩ�����ݿ�**

ע��ʱ���Զ��������� DbContext �� `EnsureDatabaseMigrated()`�������ֶ�Ǩ�ơ�

5. **ʹ�òִ���������**
public class MyService
{
    private readonly MyEntityRepository _repo;
    public MyService(MyEntityRepository repo) { _repo = repo; }
    // ...
}
## �����÷�
- ֧���Զ���ִ���DTO ӳ�䡢ȫ�ֲ�ѯ���˵�
- ���Դ��ע���������չ����

## ���֤
MIT
