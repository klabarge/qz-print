/**
 * Echoes the signed message and exits
 */
 
<%@ Page Language="VB" Explicit="True" validateRequest="false" Strict="False" debug="True" trace="False" %>
<%@ Import namespace="System.Security.Cryptography.X509Certificates"%>
<%@ Import namespace="System.Security.Cryptography"%>
<%@ Import namespace="System.Text.Encoding"%>
<script runat="server">

     // #########################################################
     // #             WARNING   WARNING   WARNING               #
     // #########################################################
     // #                                                       #
     // # This file is intended for demonstration purposes      #
     // # only.                                                 #
     // #                                                       #
     // # It is the SOLE responsibility of YOU, the programmer  #
     // # to prevent against unauthorized access to any signing #
     // # functions.                                            #
     // #                                                       #
     // # Organizations that do not protect against un-         #
     // # authorized signing will be black-listed to prevent    #
     // # software piracy.                                      #
     // #                                                       #
     // # -QZ Industries, LLC                                   #
     // #                                                       #
     // #########################################################

    // Sample key.  Replace with one used for CSR generation
    Sub Page_Load(sender As Object, e As EventArgs) 
    Dim strMessage as String = Request("request")
	
    Dim KEY         As String = Server.mappath("private-key.pfx")
    Dim PASS        As String = "S3cur3P@ssw0rd"

    Dim cert        As X509Certificate2 = New X509Certificate2(KEY, PASS, X509KeyStorageFlags.MachineKeySet)
    Dim csp         As RSACryptoServiceProvider = CType(cert.PrivateKey,RSACryptoServiceProvider)
	  Dim shaM        As New SHA1Managed()

    Dim encodedData As New System.Text.AsciiEncoding
    Dim data()      As Byte = encodedData.GetBytes(strMessage)
    Dim hash()      As Byte = shaM.ComputeHash(data)
    Dim result()    As Byte = csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"))
	
    Response.ContentType = "text/plain"
    response.write(System.Convert.ToBase64String(result,0, result.Length))
  End Sub
</script>
