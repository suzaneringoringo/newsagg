
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication2.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>NEWSs | News Aggregator</title>
	<link rel="shortcut icon" href="img/icon.png"/>
	<style>
		#map {
        margin : auto;
      }
      html, body {
        height: 100%;
        margin: 0;
        padding: 0;
        font-family : Arial , Helvetica, sans-serif;
		cursor:url("img/kursor.png"),auto;
      }
      #logo {
		margin : 0 auto;
		text-align : center;
	  }
	  #navigation {
		text-align : center;
        color :#cccccc;
		background : #000000;
		height : 35px;
		line-height: 35px;
		vertical-align: middle;
	  }
      #imglogo {
		height : 230px;
	  }
		
	  a {
		text-decoration : none;
	  }

      a:link {
            color:#000000;
            font-weight :bold;
        }

        a:visited {
            color:#000000;
            font-weight :bold;
        }
         a:hover {
            color:#a2a2a2;             
            font-weight :bold;
         }
         a:active {
            color:#cccccc;             
            font-weight :bold;
         }
		#navigation > a:link, a:visited {
             color: #cccccc;
         }

        #navigation > a:hover {
                color: #ffffff;
            }

        #navigation > a:active {
                color: #a2a2a2;
            }
		
		#footer {
            position: relative;
			text-align : center;
			background : #000000;
			color : #ffffff;
			bottom :0;
			height : 150px;
			line-height: 35px;
			vertical-align: middle;
			font-family : "Georgia", serif;
		}
		
		#top {
		  background: none;
		  margin : 0;
		  position : fixed;
		  bottom : 0;
		  right : 0;
		  z-index : 100;
		  display : none;
		  text-decoration : none;
		  color: #ffffff;
		  background-color : none;
		}
		
		#maskot1 {
			position:absolute; 
			left:10px; 
			top:30px;
		}

		#maskot2 {
			position:absolute; 
			right:10px; 
			top:30px;
		}
		
        form {
            text-align:center;
        }

        .span {
            background:#ffffff;
            padding:3px;
            border-radius:13px;
        }
     
	</style>
</head>
<body>
	<section>
		<header>
			<div id="logo"><img id="imglogo" src="img/logo.png" /></div>
			<img id="maskot1" src = "img/maskot1.gif"/>
			<img id="maskot2" src="img/maskot2.gif"/> 
		</header>
		
		<nav>
			<div id="navigation">
				<a href = "WebForm2.aspx" style="color:#ffffff;"><u>Home</u></a>
		        &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;		
				<a href = "about.html">About us</a>
			</div>
		</nav>
		
	<div id="main">
	<div id="form">

    <form id="form2" runat="server">
        <div class="span"></div>
        <br/>
		<asp:Label ID="LabelKeyword" runat="server" style="display:inline-block; z-index: 1; height: 19px" Text="Keyword : "></asp:Label>
		<asp:TextBox ID="SearchBox" runat="server" style="display:inline-block; z-index: 1;"></asp:TextBox>
	    <br/>
		<br/>
		<asp:RadioButtonList ID="RadioButtonList1" runat="server" style="text-align:left; display:inline-block; z-index: 1; height: 27px; width: 151px;">
            <asp:ListItem Selected="True ">Boyer-Moore</asp:ListItem>
            <asp:ListItem>KMP</asp:ListItem>
            <asp:ListItem>Regex</asp:ListItem>
        </asp:RadioButtonList>
        <br/>
        <br/>
        <br/>
        <br/>

        <div class="span"></div>

	    <asp:Button ID="Cari" runat="server" style="display:inline-block;z-index: 1;" Text="Cari!" OnClick="Cari_Click" />
        <div class="span"></div>
        <br/>
		
        <p id="result" runat="server"></p>
        <div id="hasil">    
            <ol id="list" runat="server" style="display:inline-block; z-index: 1; height: 19px; width: 576px; text-align: left ">
            
            </ol>
        </div>
	</form>
        <div class="span"></div>
        
	</div>
    </div>
</section>
    <div class="span"></div>
    <div id="map"></div>
      <footer>
			<div id="footer">
				<p><br/> "Tidak ada cara yang terbaik, tetapi selalu ada cara yang lebih baik." - Rinaldi Munir, 2009.
				<br/> &copy; ValakRingo2Nyo 
				</p>
			</div>
		</footer>
    <script type="text/javascript" lang="javascript">
      
      function geocodeAddress() {
        var geocoder = new google.maps.Geocoder();
        var address = document.getElementById('SearchBox').value;
        if (address) {
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status === 'OK') {
                    document.getElementById('map').style.width = "30%";
                    document.getElementById('map').style.height = "30%";
                    var map = new google.maps.Map(document.getElementById('map'), {
                        zoom: 8,
                        center: results[0].geometry.location
                    });
                    var marker = new google.maps.Marker({
                        map: map,
                        position: results[0].geometry.location
                    });
                }
            });
        }
      }
    </script>
    <script  async defer
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBOERYb1iBIgutymykmK7YaAMdhpP9USVA&callback=geocodeAddress">
    </script>

<a id="top" style ="display:inline;" href="#"><img src="img/top.png"/></a>
</body>
</html>
