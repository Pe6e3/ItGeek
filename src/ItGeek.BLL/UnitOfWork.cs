using ItGeek.BLL.Repositories;
using ItGeek.DAL.Data;

namespace ItGeek.BLL;

public class UnitOfWork
{
    private readonly AppDbContext _db;

    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }

    private AuthorRepository _authorRepository;
    private AuthorSocialRepository _authorSocialRepository;
    private CategoryRepository _categoryRepository;
    private CommentRepository _commentRepository;
    private MenuItemRepository _menuItemRepository;
    private MenuRepository _menuRepository;
    private PostContentRepository _postContentRepository;
    private PostRepository _postRepository;
    private RoleRepository _roleRepository;
    private TagRepository _tagRepository;
    private UserProfileRepository _userProfileRepository;
    private UserRepository _userRepository;
    private PostCategoryRepository _postCategoryRepository;
    private PostAuthorRepository _postAuthorRepository;
    private PostTagRepository _postTagRepository;



    public AuthorRepository AuthorRepository => _authorRepository ??= new AuthorRepository(_db);
    
    public AuthorSocialRepository AuthorsSocialRepository
    {
        get
        {
            if (_authorSocialRepository == null)
            {
                _authorSocialRepository = new AuthorSocialRepository(_db);
            }
            return _authorSocialRepository;
        }
    }


    public CategoryRepository CategoryRepository
    {
        get
        {
            if (_categoryRepository == null)
            {
                _categoryRepository = new CategoryRepository(_db);
            }
            return _categoryRepository;
        }
    }
    public CommentRepository CommentRepository
    {
        get
        {
            if (_commentRepository == null)
            {
                _commentRepository = new CommentRepository(_db);
            }
            return _commentRepository;
        }
    }
    public PostRepository PostRepository
    {
        get
        {
            if (_postRepository == null)
            {
                _postRepository = new PostRepository(_db);
            }
            return _postRepository;
        }
    }

    public PostContentRepository PostContentRepository
    {
        get
        {
            if (_postContentRepository == null)
            {
                _postContentRepository = new PostContentRepository(_db);
            }
            return _postContentRepository;
        }
    }

    public RoleRepository RoleRepository
    {
        get
        {
            if (_roleRepository == null)
            {
                _roleRepository = new RoleRepository(_db);
            }
            return _roleRepository;
        }
    }
    public TagRepository TagRepository
    {
        get
        {
            if (_tagRepository == null)
            {
                _tagRepository = new TagRepository(_db);
            }
            return _tagRepository;
        }
    }
    public UserRepository UserRepository
    {
        get
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository(_db);
            }
            return _userRepository;
        }
    }

    public MenuRepository MenuRepository
    {
        get
        {
            if (_menuRepository == null)
            {
                _menuRepository = new MenuRepository(_db);
            }
            return _menuRepository;
        }
    }
    public MenuItemRepository MenuItemRepository
    {
        get
        {
            if (_menuItemRepository == null)
            {
                _menuItemRepository = new MenuItemRepository(_db);
            }
            return _menuItemRepository;
        }
    }
    public UserProfileRepository UserProfileRepository
    {
        get
        {
            if (_userProfileRepository == null)
            {
                _userProfileRepository = new UserProfileRepository(_db);
            }
            return _userProfileRepository;
        }
    }

    public PostCategoryRepository PostCategoryRepository
    {
        get
        {
            if (_postCategoryRepository == null)
            {
                _postCategoryRepository = new PostCategoryRepository(_db);
            }
            return _postCategoryRepository;
        }
    }

    public PostAuthorRepository PostAuthorRepository
    {
        get
        {
            if (_postAuthorRepository == null)
            {
                _postAuthorRepository = new PostAuthorRepository(_db);
            }
            return _postAuthorRepository;
        }
    }


    public PostTagRepository PostTagRepository
    {
        get
        {
            if (_postTagRepository == null)
            {
                _postTagRepository = new PostTagRepository(_db);
            }
            return _postTagRepository;
        }
    }
}