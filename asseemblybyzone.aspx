<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="asseemblybyzone.aspx.cs" Inherits="exam.asseemblybyzone"
    ViewStateEncryptionMode="Always" %>
 <style> 
     .card-outline {
    border-top: 3px solid #007bff;
    border-bottom:3px solid #007bff;
} 
 </style> 

                <table id="tblboothlist1" class="table table-head-fixed text-nowrap card-outline">
    <thead class="f-size-sm f-color-secondary border-default border-b text-left">
        <tr >
            <th class="p-xs" colspan="7"><asp:Literal runat="server" Text="Assembly  List" /></th>
        </tr>
    </thead>
    <%if (BoothList != null)
        {
            if (BoothList.Tables.Count > 0)
            {
                if (BoothList.Tables[0].Rows.Count > 0)
                {
                for (int i = 0; i < BoothList.Tables[0].Rows.Count; i++)
                {
    %>
    <tbody>
        <tr class='bg-white'>
            <td class="py-xs px-sm" style="width: 7%;">&nbsp;</td>
            <td class="py-xs px-sm" style="width: 18%;">
                <div class="flex-stretch f-color-black f-size-sm md:f-size-sm flex-col justify-center text-left capitalize">
                    <h2 class="f-size-2xs md:f-size-2xs"><a class='btn btn-primary btnwidth2' style="display:block" data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["acname"].ToString() %>'>
                        <%=BoothList.Tables[0].Rows[i]["acname"].ToString().Length > 20 ? BoothList.Tables[0].Rows[i]["acname"].ToString().PadRight(140).Substring(0, 20).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["acname"].ToString()%>
                    </a></h2>
                </div>
            </td>
            <td class="py-xs px-sm" style="width: 15%;">
                <div class="flex-stretch bg-warning f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-primary capitalize">
                    <h2 class="f-size-sm md:f-size-sm"><a class='btn whiteText btnwidth2 cursor-pointer' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["acname"].ToString() %>'>
                         <%=BoothList.Tables[0].Rows[i]["TotalCount"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["TotalCount"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["TotalCount"].ToString()%>
                    </a></h2>
                </div>
            </td>
               
            <td class="py-xs px-sm" style="width: 15%;">
                <div class="flex-stretch  bg-success f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-success capitalize">
                    <h2 class="f-size-sm md:f-size-sm"><a class='btn btn-success btnwidth2 cursor-pointer' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["acname"].ToString() %>'>
                         <%=BoothList.Tables[0].Rows[i]["InstalledCount"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["InstalledCount"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["InstalledCount"].ToString()%>
                    </a></h2>
                </div>
            </td>
            
            <td class="py-xs px-sm" style="width: 15%;">
                <div class="offlinediv flex-stretch f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-warning capitalize">
                    <h2 class="f-size-sm md:f-size-sm"><a class='btn whiteText btnwidth2 cursor-pointer' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["acname"].ToString() %>'>
                          <%=BoothList.Tables[0].Rows[i]["UninstalledCount"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["UninstalledCount"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["UninstalledCount"].ToString()%>
                    </a></h2>
                </div>
            </td>
           
         
        </tr>
    </tbody>
    <%}
        }
        else
        {%>
    <tbody>
        <tr class='bg-white'>
            <td colspan="7">No Record found.</td>
        </tr>
    </tbody>
    <%}
        }
        else
        {%>
    <tbody>
        <tr class='bg-white'>
            <td colspan="7">No Record found.</td>
        </tr>
    </tbody>
    <%}
            
        }else
        {%>
    <tbody>
        <tr class='bg-white'>
            <td colspan="7">No Record found.</td>
        </tr>
    </tbody>
    <%} %>
</table> 
