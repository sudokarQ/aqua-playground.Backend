OrderService:
	
        Task CreateAsync(OrderPostDto shop);
        Task RemoveAsync(IdDto dto);
        Task UpdateAsync(OrderPutDto dto);
        Task<List<OrderGetDto>> GetAllAsync();
        Task<List<OrderGetDto>> FindByIdAsync(IdDto dto);
        Task<List<OrderSearchGetDto>> GetListByUserIdAsync(IdDto dto);
        Task<List<OrderSearchGetDto>> GetClientCart(IdDto dto);
        Task<List<OrderSearchGetDto>> GetListByDatesAsync(DateTime? begin, DateTime? end);

PromotionService:

        Task CreateAsync(PromotionPostDto promotion);
        Task<List<PromotionGetDto>> GetAllAsync();
        Task<List<PromotionSearchGetDto>> GetListByNameAsync(string name);
        Task<List<PromotionGetDto>> FindByIdAsync(IdDto dto);
        Task<List<PromotionGetDto>> FindByNameAsync(string name);
        Task RemoveAsync(IdDto dto);
        Task UpdateAsync(PromotionPutDto dto);

OrderPromotionService:

        Task CreateAsync(OrderPromotionPostDto shop);
        Task<List<OrderPromotionGetDto>> GetAllAsync();
        Task<List<OrderPromotionGetDto>> FindAsync(Guid orderId, Guid promotionId);
        Task<List<OrderPromotionGetDto>> FindByOrderId(IdDto dto);

ServiceService:

        Task CreateAsync(ServicePostDto service);
        Task<List<ServiceGetDto>> FindByIdAsync(IdDto dto);
        Task RemoveAsync(IdDto dto);
        Task UpdateAsync(ServicePutDto dto);
        Task<List<ServiceGetDto>> GetAllAsync();
        Task<List<ServiceSearchGetDto>> GetListByNameAsync(string name);

UserService: 

        Task CreateAsync(UserPostDto user);
        Task<List<UserGetDto>> FindByIdAsync(IdDto dto);
        Task<List<UserSearchGetDto>> GetListByLoginAsync(string login);
        Task RemoveAsync(IdDto dto);
        Task UpdateAsync(UserPutDto dto);
        Task<List<UserGetDto>> GetAllAsync();