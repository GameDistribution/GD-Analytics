<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="analytcs_Default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>GameDistribution Analytics</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Styles -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen">
    <link rel="stylesheet" href="css/bootswatch.min.css">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->


    <style type="text/css">
        
        .form-signin
        {
            max-width: 300px;
            padding: 19px 29px 29px;
            margin: 0 auto 20px;
            background-color: #fff;
            border: 1px solid #e5e5e5;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            box-shadow: 0 1px 2px rgba(0,0,0,.05);
        }
        .form-signin .form-signin-heading, .form-signin .checkbox
        {
            margin-bottom: 10px;
        }
        .form-signin input[type="text"], .form-signin input[type="password"]
        {
            font-size: 16px;
            height: auto;
            margin-bottom: 15px;
            padding: 7px 9px;
        }
    </style>
    <link href="css/fgs.css" rel="stylesheet">
    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="img/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="img/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="img/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="img/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="img/ico/favicon.png">
</head>
<body role="document">
<form runat="server" id="LoginForm" method="post">

    <div class="navbar navbar-default navbar-fixed-top" >
        <div class="container-fluid">
        <div class="navbar-header">
            <a class="navbar-brand" href="#">Gd Analytics</a>
        </div>
        </div>
    </div>

    <div style="top: 50%; position: absolute; margin-top: -8em; left: 50%; margin-left: -10em;">
        <div class="form-signin" style="width:40em;">
            <h2 class="form-signin-heading">
                Please sign in</h2>
            <input type="text" class="form-control" id="uemail" name="uemail" placeholder="Email address">
            <input type="password" class="form-control" id="upassword" name="upassword" placeholder="Password">
            <button class="btn btn-large btn-primary" data-loading-text="Signing..." type="submit"
                id="btn_login">Sign in</button>
            <hr />                
            <div><span class="loader" id="loader" style="display:none"/></div><div id="alert_placeholder"></div>
        </div>            
    </div>        

    <div class="container-fluid" role="main">
        <div class="modal fade" id="myModal">
            <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3>Information</h3>
                  </div>
                  <div class="modal-body">
                    <p>Email address or Password cannot be empty!</p>
                  </div>
                  <div class="modal-footer">
                    <a href="#" class="btn" onclick="$('#myModal').modal('hide')">Close</a>
                  </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /container -->

    <!-- Placed at the end of the document so the pages load faster -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/bootswatch.js"></script>

    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="js/flot/excanvas.min.js"></script><![endif]-->
    <script>
        $(document).ready(function () {

            $('#LoginForm').submit(function (event) {
                // Stop full page load
                $("#btn_login").attr("disabled", "disabled");
                event.preventDefault();
                //$(event.currentTarget).attr("disabled", "disabled");

                if ($('#uemail').val().length > 0 && $('#upassword').val().length > 0) {
                    $("#loader").show();

                    fetchTimer = Math.round((new Date()).getTime() / 1000);
                    // Request
                    var data = { uemail: $('#uemail').val(), upassword: $('#upassword').val() };

                    $.ajax({
                        url: "Login.aspx",
                        dataType: 'json',
                        type: 'POST',
                        data: data,
                        success: onDataReceived,
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            $("#btn_login").removeAttr("disabled");
                        }
                    });
                } else {
                    $("#btn_login").removeAttr("disabled");
                    $('#myModal').modal('show');
                    $("#loader").hide();
                }
            });


            function onDataReceived(recv) {
                if (recv) {
                    document.location.href = "RealTimeDashboard.aspx";
                }
                else {
                    showAlert("Please check your logins!");
                    $("#btn_login").removeAttr("disabled");
                    $("#loader").hide();
                }
            }

            function showAlert(msg) {
                $("#alert_placeholder").append('<div class="alert alert-block alert-error fade in"><button type="button" class="close" data-dismiss="alert">×</button><strong>' + msg + '</strong></div>');
            }

        });
    </script>
</form>
</body>
</html>
