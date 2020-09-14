<h1>WELCOME</h1>
<p>
  This is a basic web application for buying art products.
</p>
<p>
  This web application was built with ASP.NET Core following a tutorial on Pluralsight by Shawn Wildermuth.<br>
  The tutorial can be found here: https://app.pluralsight.com/library/courses/aspnetcore-mvc-efcore-bootstrap-angular-web/table-of-contents
</p>

<h2>API</h2>
<p>
  To have permission to make an API request, you must have an account. You must provide your username and password in the body to make the CreateToken API request.
  The CreateToken API request should return a temporary token which expires in 30 minutes.
  After you get the token, add the value "Bearer [<i>insert token here</i>]" to the key "Authorization" for the Headers of the API request.
</p>
<ul>
  <li>POST http://localhost:8888/account/CreateToken</li>
  <li>POST http://localhost:8888/api/orders</li>
  <li>GET http://localhost:8888/api/orders (?includeItems=true/false)</li>
  <li>GET http://localhost:8888/api/orders/#</li>
  <li>GET http://localhost:8888/api/orders/#/items</li>
  <li>GET http://localhost:8888/api/orders/#/items/#</li>
  <li>GET http://localhost:8888/api/products</li>
</ul>
