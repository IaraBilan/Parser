$session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
$session.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Mobile Safari/537.36"
$r = Invoke-WebRequest -UseBasicParsing -Uri "https://www.lesegais.ru/open-area/graphql" `
-Method "POST" `
-WebSession $session `
-Headers @{
"sec-ch-ua"="`" Not A;Brand`";v=`"99`", `"Chromium`";v=`"96`", `"Google Chrome`";v=`"96`""
  "Accept"="*/*"
  "sec-ch-ua-mobile"="?1"
  "sec-ch-ua-platform"="`"Android`""
  "Origin"="https://www.lesegais.ru"
  "Sec-Fetch-Site"="same-origin"
  "Sec-Fetch-Mode"="cors"
  "Sec-Fetch-Dest"="empty"
  "Referer"="https://www.lesegais.ru/open-area/deal"
  "Accept-Encoding"="gzip, deflate, br"
  "Accept-Language"="ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7,uk;q=0.6"
} `
-ContentType "application/json" `
-Body "{`"query`":`"query SearchReportWoodDeal(`$size: Int!, `$number: Int!, `$filter: Filter, `$orders: [Order!]) {\n  searchReportWoodDeal(filter: `$filter, pageable: {number: `$number, size: `$size}, orders: `$orders) {\n    content {\n      sellerName\n      sellerInn\n      buyerName\n      buyerInn\n      woodVolumeBuyer\n      woodVolumeSeller\n      dealDate\n      dealNumber\n      __typename\n    }\n    __typename\n  }\n}\n`",`"variables`":{`"size`":77,`"number`":0,`"filter`":null,`"orders`":null},`"operationName`":`"SearchReportWoodDeal`"}"
 $r.RawContent