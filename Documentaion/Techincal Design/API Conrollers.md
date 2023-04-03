AuthController:

	Task<IActionResult> Register(string email, string password)
	Task<IActionResult> Login(string email, string password, bool rememberMe)
	Task<IActionResult> Logout()
	Task<IActionResult> IsAuthorized(string email, string policy)

OrderController :

	Task<List<OrderGetDto>> GetAllAsync()
	Task<IActionResult> CreateAsync(OrderPostDto orderPostDto)
	Task<IActionResult> Remove(IdDto dto)
	Task<IActionResult> Update(OrderPutDto dto)
	Task<List<OrderGetDto>> FindByIdAsync(IdDto dto)
	Task<List<OrderSearchGetDto>> FindByUserIdAsync(IdDto dto)
	Task<List<OrderSearchGetDto>> GetListByDatesAsync(DateTime? begin, DateTime? end)

PromotionController:
	
	Task<List<OrderGetDto>> GetAllAsync()
	Task<IActionResult> CreateAsync(OrderPostDto orderPostDto)
	Task<IActionResult> Remove(IdDto dto)
	Task<IActionResult> Update(OrderPutDto dto)
	Task<List<OrderGetDto>> FindByIdAsync(IdDto dto)
	Task<List<PromotionSearchGetDto>> GetListByName(string name)

ServiceController:
	
	Task<List<OrderGetDto>> GetAllAsync()
	Task<IActionResult> CreateAsync(OrderPostDto orderPostDto)
	Task<IActionResult> Remove(IdDto dto)
	Task<IActionResult> Update(OrderPutDto dto)
	Task<List<OrderGetDto>> FindByIdAsync(IdDto dto)
	Task<List<PromotionSearchGetDto>> GetListByName(string name)

UserController:

	Task<List<OrderGetDto>> GetAllAsync()
	Task<IActionResult> CreateAsync(OrderPostDto orderPostDto)
	Task<IActionResult> Remove(IdDto dto)
	Task<IActionResult> Update(OrderPutDto dto)
	Task<List<OrderGetDto>> FindByIdAsync(IdDto dto)
	Task<List<PromotionSearchGetDto>> GetListByName(string name)