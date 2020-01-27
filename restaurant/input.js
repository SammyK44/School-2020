function submitCheck()
{
  if (document.getElementById("name").value == "") {alert("Please enter your name."); return false;}
  if (document.getElementById("email").value == "") {alert("Please enter your email."); return false;}
  if (document.getElementById("phone").value == "") {alert("Please enter your phone number."); return false;}
  if (document.getElementById("mon" && "tue" && "wed" && "thu" && "fri").value == "") {alert("Please select available day(s)."); return false;}
  confirm("Is all the order information correct?");
  //Send to server
}
