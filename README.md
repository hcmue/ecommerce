# ecommerce
E-Commerce resource

## Payment
* OnePay https://mtf.onepay.vn/developer/?page=modul_noidia
* VNPay https://sandbox.vnpayment.vn/apis/downloads
* Bảo Kim http://sandbox.baokim.vn/ and https://developer.baokim.vn/payment/
* Paypal

## Live Chat
* Tawk to: https://www.tawk.to/
* Subiz https://subiz.com/vi/
* ZenDesk https://www.zendesk.com/
* FB

## Url friendly
Sử dụng route để định tuyến url dạng như sau cho tất cả trang chi tiết sản phẩm:

* host/dien-thoai/samsung-galaxy-note-9-64gb-gold

## Open Graph meta tags hỗ trợ các social
Ví dụ cho FB:
```
<meta property="og:url"                content="http://www.nytimes.com/2015/02/19/arts/international/when-great-minds-dont-think-alike.html" />
<meta property="og:type"               content="article" />
<meta property="og:title"              content="When Great Minds Don’t Think Alike" />
<meta property="og:description"        content="How much does culture influence creative thinking?" />
<meta property="og:image"              content="http://static01.nyt.com/images/2015/02/19/arts/international/19iht-btnumbers19A/19iht-btnumbers19A-facebookJumbo-v2.jpg" />
```

* Bước 1: Thêm section ở trong layout template
```
@RenderSection("metasocials", required:false)
```

* Bước 2: Ở mỗi trang định nghĩa phần section này
```
@section metasocials{
	<meta property="og:url" content="" />
	<meta property="og:type" content="" />
	<meta property="og:title" content="" />
	<meta property="og:description" content="" />
	<meta property="og:image" content="" />
}
```
