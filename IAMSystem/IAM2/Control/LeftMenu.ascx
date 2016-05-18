<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="CCBMP.BackEnd.Control.Left1" %>
<!--  左边样式 -->
<div class="sidebar" id="leftcontent">
    <ul class="column" style="width: 170px">
        <%if (UserRoleModule.AD || UserRoleModule.Admin || UserRoleModule.EHR || UserRoleModule.TC || UserRoleModule.SAP || UserRoleModule.HEC || UserRoleModule.Leader)
          { %>

        <li class="column">

            <a href="javascript:loadecontent('admin/HREmployeeManager.aspx','');" class="col1" onclick="queryclose()">
                <img src="../style/images/mjia.gif" />
                员工基本信息</a>
        </li>
        <%} %>
        <% if (UserRoleModule.AD == true)
           { %>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    域控

                </a>
                <li class="column2"><a href="javascript:loadecontent('Admin/AdUserInfoManager.aspx','');" class="col2" onclick="queryopen()">账号管理           
                </a></li>

                <li class="column2"><a href="javascript:loadecontent('Report/ADUserReport.aspx','');" class="col2" onclick="queryopen()">账号报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Report/ADGroupReport.aspx','');" class="col2" onclick="queryopen()">角色报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Admin/ADDefaultWorkGroupManager.aspx','');" class="col2" onclick="queryopen()">默认角色管理           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Admin/Ad_department_workgroupManager.aspx','');" class="col2" onclick="queryopen()">部门角色管理           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Admin/Ad_Zhiji_workgroupManager.aspx','');" class="col2" onclick="queryopen()">职级角色管理           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Admin/Ad_workgroupManager.aspx','');" class="col2" onclick="queryopen()">可控角色管理           
                </a></li>
            </ul>
        </li>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    计算机

                </a>
                <li class="column2"><a href="javascript:loadecontent('Admin/AD_ComputerManager.aspx','');" class="col2" onclick="queryopen()">账号管理          
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Report/ADComputerReport.aspx?type=adcomputer','');" class="col2" onclick="queryopen()">账号报表          
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Report/ADComputerReport.aspx?type=workgroup','');" class="col2" onclick="queryopen()">角色报表          
                </a></li>
            </ul>
        </li>
        <%} %>

        <%if (UserRoleModule.EHR == true)
          { %>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    人事管理系统</a>

                <li class="column2"><a href="javascript:loadecontent('Admin/HrsmUserManager.aspx','');" class="col2" onclick="queryopen()">账号管理          
                </a></li>

                <li class="column2"><a href="javascript:loadecontent('report/hruserreport.aspx','');" class="col2" onclick="queryopen()">账号报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/HrRoleReport.aspx','');" class="col2" onclick="queryopen()">角色报表          
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('rolelist/HrRoleList.aspx','');" class="col2" onclick="queryopen()">角色信息列表           
                </a></li>

                <li class="column2"><a href="javascript:loadecontent('rolelist/HRCompanyManager.aspx','');" class="col2" onclick="queryopen()">公司信息列表           
                </a></li>
            </ul>
        </li>
        <%} %>

        <%if (UserRoleModule.SAP == true)
          {%>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    SAP</a>

                <li class="column2"><a href="javascript:loadecontent('Admin/sapUserManger.aspx','');" class="col2" onclick="queryopen()">账号管理         
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/sapuserreport.aspx','');" class="col2" onclick="queryopen()">账号报表         
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/saprolereport.aspx','');" class="col2" onclick="queryopen()">角色报表        
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('rolelist/SapRoleList.aspx','');" class="col2" onclick="queryopen()">角色信息列表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Admin/SAP_ParametersSettingManager.aspx','');" class="col2" onclick="queryopen()">默认参数设置           
                </a></li>

                <li class="column2"><a href="javascript:loadecontent('Admin/SAPUserparmenterEdit.aspx','');" class="col2" onclick="queryopen()">批量设置           
                </a></li>
            </ul>
        </li>
        <%} %>

        <%if (UserRoleModule.TC)
          { %>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    Team Center</a>
                <li class="column2"><a href="javascript:loadecontent('admin/TCUserInfoManager.aspx','');" class="col2" onclick="queryopen()">账号管理           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Report/TCUserReportnew.aspx','');" class="col2" onclick="queryopen()">账号报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Report/TCRoleReport.aspx','');" class="col2" onclick="queryopen()">角色报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('rolelist/TcRoleList.aspx','');" class="col2" onclick="queryopen()">角色信息列表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('admin/TCLicense.aspx','');" class="col2" onclick="queryopen()">TC License配置           
                </a></li>
            </ul>
        </li>
        <%} %>

        <%if (UserRoleModule.HEC)
          { %>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    预算管理系统</a>
                <li class="column2"><a href="javascript:loadecontent('Admin/HECUserInfoManager.aspx','');" class="col2" onclick="queryopen()">账号管理           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/HECUserReport.aspx','');" class="col2" onclick="queryopen()">账号报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/HECRoleReport.aspx','');" class="col2" onclick="queryopen()">角色报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/HECUserGangwei.aspx?user=1','');" class="col2" onclick="queryopen()">账号岗位报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/HECUserGangwei.aspx?user=0','');" class="col2" onclick="queryopen()">岗位账号报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('rolelist/HECRoleList.aspx','');" class="col2" onclick="queryopen()">角色信息列表           
                </a></li>

                <li class="column2"><a href="javascript:loadecontent('admin/HECCompanyManager.aspx','');" class="col2" onclick="queryopen()">公司信息列表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('rolelist/HECGangweiList.aspx','');" class="col2" onclick="queryopen()">岗位信息列表           
                </a></li>
            </ul>
        </li>
        <%} %>




        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    报表</a>
                <li class="column2"><a href="javascript:loadecontent('report/ReportByUserCode.aspx','');" class="col2" onclick="queryopen()">用户角色报表           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/lizhibaobiao.aspx','');" class="col2" onclick="queryopen()">离职报表           
                </a></li>
            </ul>
        </li>
        <%if (UserRoleModule.Admin || UserRoleModule.Leader)
          { %>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    岗位模版</a>
                <li class="column2"><a href="javascript:loadecontent('admin/RoleTemplateManger.aspx','');" class="col2" onclick="queryopen()">模版列表           
                </a></li>
            </ul>
        </li>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    差异分析详情</a>
                <li class="column2"><a href="javascript:loadecontent('report/DifferenceReport.aspx','');" class="col2" onclick="queryopen()">用户与用户差异分析           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/DifferenceReportByTemplateName.aspx','');" class="col2" onclick="queryopen()">模版与模版差异分析           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('report/DifferenceReportByUserAndTemplate.aspx','');" class="col2" onclick="queryopen()">用户与模版差异分析           
                </a></li>
            </ul>
        </li>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    冲突解决</a>
                <li class="column2">
                    <a href="javascript:loadecontent('ConflictResolution/HRChayi.aspx','');" class="col2" onclick="queryopen()">员工差异列表           
                    </a>

                </li>
                <li class="column2">
                    <a href="javascript:loadecontent('ConflictResolution/HRUserResolution1.aspx','');" class="col2" onclick="queryopen()">员工冲突列表           
                    </a>

                </li>
                <li class="column2">
                    <a href="javascript:loadecontent('ConflictResolution/UserConflictResolution.aspx','');" class="col2" onclick="queryopen()">账号冲突列表           
                    </a>

                </li>


            </ul>
        </li>
        <li class="column1">
            <ul class="column">
                <a href="javascript:loadecontent('','');" class="col1" onclick="queryclose()">
                    <img src="../style/images/mjia.gif" />
                    系统管理</a>
                <li class="column2"><a href="javascript:loadecontent('Admin/SyncListManager.aspx','');" class="col2" onclick="queryopen()">同步管理           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Admin/LoglistManager.aspx','');" class="col2" onclick="queryopen()">日志管理           
                </a></li>
                <li class="column2"><a href="javascript:loadecontent('Admin/UserRoleManager.aspx','');" class="col2" onclick="queryopen()">系统权限管理           
                </a></li>

                <li class="column2"><a href="javascript:loadecontent('Admin/TaskEmailManager.aspx','');" class="col2" onclick="queryopen()">系统邮箱管理           
                </a></li>

            </ul>
        </li>
        <%} %>
    </ul>
</div>
<script type="text/javascript" language="javascript">
    function loadecontent(url, _Id) {
        this.document.getElementById("contentiframe").src = url;
        AjaxMenuUsage(_Id);
    }
    jQuery(function () {
        jQuery("ul>a").each(function () {
            jQuery(this).parent().find("li").slideUp("normal");
        });

        jQuery(".chack").parent().find("li").slideToggle("normal");



        jQuery("ul>a").bind("click", function (ev) {
            var athis = this;

            //alert(athis);
            jQuery("ul>a").each(function () {
                if (this != athis) {
                    jQuery(this).parent().find("li").slideUp("normal");
                    //                    jQuery(this).parent().find("li>a").removeClass("chack");
                    jQuery(this).removeClass("chack");
                    jQuery(this).find("img").attr({ src: "../images/mjia.gif" });
                }

            });
            jQuery(this).parent().find("li").slideToggle("normal");
            jQuery(this).addClass("chack");
            if (jQuery(this).find("img").attr("src") == "../images/mjian.gif") {

                jQuery(this).find("img").attr({ src: "../images/mjia.gif" });
            } else {
                jQuery(this).find("img").attr({ src: "../images/mjian.gif" });
            }
        });
        jQuery("li>a").bind("click", function (ev) {

            jQuery("ul>a,li>a").removeClass("chack");
            jQuery(this).addClass("chack");
            jQuery(this).parent().parent().parent().find("ul>a").addClass("chack");
            jQuery(this).parent().siblings().find("ul>a").removeClass("chack");

        });

    });

    function AjaxMenuUsage(_Id) {
        //        jQuery.post(
        //        "../admin/MenuUsageManager.aspx",
        //        { Action: "post", methon: "Add", Id: _Id },
        //        function (data) {

        //        }, "Text"
        //        );
    }


    function queryclose() {
        jQuery("#idDrag").css('display', 'none');
        //document.getElementById("idDrag").style.display = "none";
    }
    function queryopen() {
        jQuery("#idDrag").css('display', 'block');
        //document.getElementById("idDrag").style.display = "";
    }

    //        $().ready(function () {
    //            $("#DivAmiis>a").each(function (i) {
    //                $(this).bind("click", function () {
    //                    if ($(this).text() == "AML信息维护") {
    //                        $(".column>.col1").each(function (i) {  
    //                            $(this).hide();

    //                            if ($(this).text() == "" || $(this).text() == "") {
    //                                $(this).show();
    //                            }    
    //                        });
    //                    }
    //                    else if ($(this).text() == "AML信息维护") {
    //                        
    //                    }
    //                });
    //});

    //});
</script>
