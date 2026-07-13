# EShopMicroservices - Thêm 2 Tính Năng Mới

Dự án này bổ sung 2 tính năng API mới vào hệ thống EShopMicroservices có sẵn.

## Tổng quan 2 Tính Năng

| #   | Feature                     | Service              | Endpoint                                  |
| --- | --------------------------- | -------------------- | ----------------------------------------- |
| 1   | **GetProductsByPriceRange** | Catalog.API `:6000`  | `GET /products/price?minPrice=&maxPrice=` |
| 2   | **GetOrdersByStatus**       | Ordering.API `:6003` | `GET /orders/status/{status}`             |

---

## 1. Feature 1 – GetProductsByPriceRange

**Mục đích:** Lọc danh sách sản phẩm theo khoảng giá (từ `minPrice` đến `maxPrice`).

### Cấu trúc file mới thêm vào Catalog.API

```text
Catalog.API/
└── Products/
    └── GetProductsByPriceRange/
        ├── GetProductsByPriceRangeHandler.cs
        └── GetProductsByPriceRangeEndpoint.cs
```

### Mã nguồn

**GetProductsByPriceRangeHandler.cs** (Thực hiện logic query qua Marten)
![GetProductsByPriceRangeHandler Code](images/GetProductsByPriceRangeHandler.jpg)

**GetProductsByPriceRangeEndpoint.cs** (Carter Endpoint MapGet nhận query string)
![GetProductsByPriceRangeEndpoint Code](images/GetProductsByPriceRangeEndpoint.jpg)

### Kết quả Test bằng Postman

Gọi API qua Postman để lọc sản phẩm trong khoảng giá từ 500 đến 1000:
`GET http://localhost:6000/products/price?minPrice=500&maxPrice=1000`

![GetProductsByPriceRange Postman Result](images/GetProductsByPriceRange_Postman.jpg)

---

## 2. Feature 2 – GetOrdersByStatus

**Mục đích:** Lọc danh sách orders theo trạng thái đơn hàng (ví dụ: Draft, Pending, Completed, Cancelled).

### Cấu trúc file mới thêm vào Ordering Service

```text
Ordering.Application/Orders/Queries/GetOrdersByStatus/
├── GetOrdersByStatusQuery.cs
└── GetOrdersByStatusHandler.cs

Ordering.API/Endpoints/
└── GetOrdersByStatus.cs
```

### Mã nguồn

**GetOrdersByStatusQuery.cs** (Định nghĩa Query record)
![GetOrdersByStatusQuery Code](images/GetOrdersByStatusQuery.jpg)

**GetOrdersByStatusHandler.cs** (Xử lý filter trạng thái qua EF Core DbContext)
![GetOrdersByStatusHandler Code](images/GetOrdersByStatusHandler.jpg)

**GetOrdersByStatus.cs** (Carter Endpoint nhận biến route `{status}`)
![GetOrdersByStatusEndpoint Code](images/GetOrdersByStatusEndpoint.jpg)

### Kết quả Test bằng Postman

**OrderStatus Enum Values:**

- `1` : Draft
- `2` : Pending
- `3` : Completed
- `4` : Cancelled

Kiểm tra API lấy các đơn hàng đang ở trạng thái `Pending` (`status=2`):
`GET http://localhost:6003/orders/status/2`

![GetOrdersByStatus Postman Result](images/GetOrdersByStatus_Postman.jpg)
