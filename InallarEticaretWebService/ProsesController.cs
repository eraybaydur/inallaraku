
using NotthomePortalApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace NotthomePortalApi
{
    public class ProsesController
    {
        private readonly IConfiguration _configuration;

        public ProsesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region ERP

        public string ABUDGETPRD => LG_Firma_Tablosu("ABUDGETPRD");
        public string ACCCODES => LG_Firma_Tablosu("ACCCODES");
        public string ACCCODES_210TMP => LG_Firma_Tablosu("ACCCODES_210TMP");
        public string ACCCODES_227TMP => LG_Firma_Tablosu("ACCCODES_227TMP");
        public string ACCCODES_245TMP => LG_Firma_Tablosu("ACCCODES_245TMP");
        public string ACCCODES_252TMP => LG_Firma_Tablosu("ACCCODES_252TMP");
        public string ACCCODESTMP => LG_Firma_Tablosu("ACCCODESTMP");
        public string ACCDISTTEMP => LG_Firma_Tablosu("ACCDISTTEMP");
        public string ACCDISTTEMPLN => LG_Firma_Tablosu("ACCDISTTEMPLN");
        public string ACCOUNTTEMPLATES => LG_Firma_Tablosu("ACCOUNTTEMPLATES");
        public string ACTIVITYAMNT => LG_Firma_Tablosu("ACTIVITYAMNT");
        public string ACTOVRHDDIST => LG_Firma_Tablosu("ACTOVRHDDIST");
        public string ADDTAX => LG_Firma_Tablosu("ADDTAX");
        public string ADDTAXLINE => LG_Firma_Tablosu("ADDTAXLINE");
        public string ANBUDGET => LG_Firma_Tablosu("ANBUDGET");
        public string ANBUDGETLN => LG_Firma_Tablosu("ANBUDGETLN");
        public string ANBUDGETPRD => LG_Firma_Tablosu("ANBUDGETPRD");
        public string APPPARAM => LG_Firma_Tablosu("APPPARAM");
        public string APPROVEUSER => LG_Firma_Tablosu("APPROVEUSER");
        public string ASCOND => LG_Firma_Tablosu("ASCOND");
        public string AUTOCTEMPLATE => LG_Firma_Tablosu("AUTOCTEMPLATE");
        public string AVGCURRS => LG_Firma_Tablosu("AVGCURRS");
        public string BANKACC => LG_Firma_Tablosu("BANKACC");
        public string BARCODETMP => LG_Firma_Tablosu("BARCODETMP");
        public string BNCARD => LG_Firma_Tablosu("BNCARD");
        public string BNCREDITCARD => LG_Firma_Tablosu("BNCREDITCARD");
        public string BNCREPAYTR => LG_Firma_Tablosu("BNCREPAYTR");
        public string BOMASTER => LG_Firma_Tablosu("BOMASTER");
        public string BOMLINE => LG_Firma_Tablosu("BOMLINE");
        public string BOMPARAM => LG_Firma_Tablosu("BOMPARAM");
        public string BOMREVSN => LG_Firma_Tablosu("BOMREVSN");
        public string BOMVRNTFORMULA => LG_Firma_Tablosu("BOMVRNTFORMULA");
        public string CAMPAIGN => LG_Firma_Tablosu("CAMPAIGN");
        public string CHANGELOG => LG_Firma_Tablosu("CHANGELOG");
        public string CHARASGN => LG_Firma_Tablosu("CHARASGN");
        public string CHARCODE => LG_Firma_Tablosu("CHARCODE");
        public string CHARSET => LG_Firma_Tablosu("CHARSET");
        public string CHARSETASGN => LG_Firma_Tablosu("CHARSETASGN");
        public string CHARVAL => LG_Firma_Tablosu("CHARVAL");
        public string CLCARD => LG_Firma_Tablosu("CLCARD");
        public string CLINTEL => LG_Firma_Tablosu("CLINTEL");
        public string CMPGNLINE => LG_Firma_Tablosu("CMPGNLINE");
        public string COMPANSEACC => LG_Firma_Tablosu("COMPANSEACC");
        public string COPRDBOM => LG_Firma_Tablosu("COPRDBOM");
        public string CRDACREF => LG_Firma_Tablosu("CRDACREF");
        public string CREDITLETTERS => LG_Firma_Tablosu("CREDITLETTERS");
        public string DECARDS => LG_Firma_Tablosu("DECARDS");
        public string DEDUCTLIMITS => LG_Firma_Tablosu("DEDUCTLIMITS");
        public string DEFNFLDSCARDV => LG_Firma_Tablosu("DEFNFLDSCARDV");
        public string DISCPAYLINES => LG_Firma_Tablosu("DISCPAYLINES");
        public string DISPLINE => LG_Firma_Tablosu("DISPLINE");
        public string DISPLINECOST => LG_Firma_Tablosu("DISPLINECOST");
        public string DISTLINE => LG_Firma_Tablosu("DISTLINE");
        public string DISTLIST => LG_Firma_Tablosu("DISTLIST");
        public string DISTROUTING => LG_Firma_Tablosu("DISTROUTING");
        public string DISTROUTLINE => LG_Firma_Tablosu("DISTROUTLINE");
        public string DISTTEMP => LG_Firma_Tablosu("DISTTEMP");
        public string DISTVEHICLE => LG_Firma_Tablosu("DISTVEHICLE");
        public string DIVAMAIN => LG_Firma_Tablosu("DIVAMAIN");
        public string DSPLNOPCMPPG => LG_Firma_Tablosu("DSPLNOPCMPPG");
        public string EBOOKINFO => LG_Firma_Tablosu("EBOOKINFO");
        public string EBOOKPARAMS => LG_Firma_Tablosu("EBOOKPARAMS");
        public string EMCENTER => LG_Firma_Tablosu("EMCENTER");
        public string EMGRPASS => LG_Firma_Tablosu("EMGRPASS");
        public string EMPGROUP => LG_Firma_Tablosu("EMPGROUP");
        public string EMPLOYEE => LG_Firma_Tablosu("EMPLOYEE");
        public string EMUHACC => LG_Firma_Tablosu("EMUHACC");
        public string EMUHACCSUBACCASGN => LG_Firma_Tablosu("EMUHACCSUBACCASGN");
        public string ENGCLINE => LG_Firma_Tablosu("ENGCLINE");
        public string EXCEPT => LG_Firma_Tablosu("EXCEPT");
        //public string EXCHANGE { get { return string.Format("{0}LG_EXCHANGE_{1}", this.DBPrefix, FirmaNoStr); } }
        public string EXIMBUSTYP => LG_Firma_Tablosu("EXIMBUSTYP");
        public string EXPCREDITCRD => LG_Firma_Tablosu("EXPCREDITCRD");
        public string EXPCREDITLN => LG_Firma_Tablosu("EXPCREDITLN");
        public string FAANNCOST => LG_Firma_Tablosu("FAANNCOST");
        public string FAEXPENSE => LG_Firma_Tablosu("FAEXPENSE");
        public string FAPRODNUMS => LG_Firma_Tablosu("FAPRODNUMS");
        public string FAREGIST => LG_Firma_Tablosu("FAREGIST");
        public string FAREGNEWVALUE => LG_Firma_Tablosu("FAREGNEWVALUE");
        public string FATRANS => LG_Firma_Tablosu("FATRANS");
        public string FAYEAR => LG_Firma_Tablosu("FAYEAR");
        public string FAYEARSTOP => LG_Firma_Tablosu("FAYEARSTOP");
        public string FICHESTATUS => LG_Firma_Tablosu("FICHESTATUS");
        public string FINTABLEITEM => LG_Firma_Tablosu("FINTABLEITEM");
        public string FINTBLHEADER => LG_Firma_Tablosu("FINTBLHEADER");
        public string FINTBLLINEACC => LG_Firma_Tablosu("FINTBLLINEACC");
        public string FIRMDOC => LG_Firma_Tablosu("FIRMDOC");
        public string FRMPRDPARAM => LG_Firma_Tablosu("FRMPRDPARAM");
        public string GAUGPARAM => LG_Firma_Tablosu("GAUGPARAM");
        public string GENMODP => LG_Firma_Tablosu("GENMODP");
        public string GERMANYDEF => LG_Firma_Tablosu("GERMANYDEF");
        public string GLASSGN => LG_Firma_Tablosu("GLASSGN");
        public string GLCONNFILT => LG_Firma_Tablosu("GLCONNFILT");
        public string GUARANTEELINE => LG_Firma_Tablosu("GUARANTEELINE");
        public string INVDEF => LG_Firma_Tablosu("INVDEF");
        public string ITEMCATEGORY => LG_Firma_Tablosu("ITEMCATEGORY");
        public string ITEMCATEGORYLINE => LG_Firma_Tablosu("ITEMCATEGORYLINE");
        public string ITEMS => LG_Firma_Tablosu("ITEMS");
        public string ITEMSUBS => LG_Firma_Tablosu("ITEMSUBS");
        public string ITMBOMAS => LG_Firma_Tablosu("ITMBOMAS");
        public string ITMCLSAS => LG_Firma_Tablosu("ITMCLSAS");
        public string ITMFACTP => LG_Firma_Tablosu("ITMFACTP");
        public string ITMLVLTMP => LG_Firma_Tablosu("ITMLVLTMP");
        public string ITMLVLTMPLN => LG_Firma_Tablosu("ITMLVLTMPLN");
        public string ITMUNITA => LG_Firma_Tablosu("ITMUNITA");
        public string ITMWSDEF => LG_Firma_Tablosu("ITMWSDEF");
        public string KSCARD => LG_Firma_Tablosu("KSCARD");
        public string LABORREQ => LG_Firma_Tablosu("LABORREQ");
        public string LEASINGPAYMENTS => LG_Firma_Tablosu("LEASINGPAYMENTS");
        public string LEASINGPAYMENTSLNS => LG_Firma_Tablosu("LEASINGPAYMENTSLNS");
        public string LEASINGREG => LG_Firma_Tablosu("LEASINGREG");
        public string LEASINGREGLN => LG_Firma_Tablosu("LEASINGREGLN");
        public string LNGEXCSETS => LG_Firma_Tablosu("LNGEXCSETS");
        public string LNOPASGN => LG_Firma_Tablosu("LNOPASGN");
        public string LOCATION => LG_Firma_Tablosu("LOCATION");
        public string LOCATIONSFOR => LG_Firma_Tablosu("LOCATIONSFOR");
        public string LOGREP => LG_Firma_Tablosu("LOGREP");
        public string MAINTANENCELINE => LG_Firma_Tablosu("MAINTANENCELINE");
        public string MARK => LG_Firma_Tablosu("MARK");
        public string MARKET => LG_Firma_Tablosu("MARKET");
        public string MBLINFOGROUP => LG_Firma_Tablosu("MBLINFOGROUP");
        public string MBLINFOUSER => LG_Firma_Tablosu("MBLINFOUSER");
        public string MBLINFUSRGRPLN => LG_Firma_Tablosu("MBLINFUSRGRPLN");
        public string MBSCRMRELF => LG_Firma_Tablosu("MBSCRMRELF");
        public string MOBILEASSET => LG_Firma_Tablosu("MOBILEASSET");
        public string MRPHEAD => LG_Firma_Tablosu("MRPHEAD");
        public string MRPITEM => LG_Firma_Tablosu("MRPITEM");
        public string MRPITEMCHG => LG_Firma_Tablosu("MRPITEMCHG");
        public string MRPLINE => LG_Firma_Tablosu("MRPLINE");
        public string MRPPEGGING => LG_Firma_Tablosu("MRPPEGGING");
        public string MRPPROPOSAL => LG_Firma_Tablosu("MRPPROPOSAL");
        public string MSGTEMPLATE => LG_Firma_Tablosu("MSGTEMPLATE");
        public string MULTIADDTAX => LG_Firma_Tablosu("MULTIADDTAX");
        public string OCCUPATION => LG_Firma_Tablosu("OCCUPATION");
        public string OFFALTER => LG_Firma_Tablosu("OFFALTER");
        public string OFFALTERSEQ => LG_Firma_Tablosu("OFFALTERSEQ");
        public string OFFFCEXCH => LG_Firma_Tablosu("OFFFCEXCH");
        public string OFFLINEEXCH => LG_Firma_Tablosu("OFFLINEEXCH");
        public string OFFTRNS => LG_Firma_Tablosu("OFFTRNS");
        public string OFFTRNSSEQ => LG_Firma_Tablosu("OFFTRNSSEQ");
        public string OPATTASG => LG_Firma_Tablosu("OPATTASG");
        public string OPERTION => LG_Firma_Tablosu("OPERTION");
        public string OPREQACTIVITY => LG_Firma_Tablosu("OPREQACTIVITY");
        public string OPRTREQ => LG_Firma_Tablosu("OPRTREQ");
        public string OVERHEADS => LG_Firma_Tablosu("OVERHEADS");
        public string OVHCDISTRATE => LG_Firma_Tablosu("OVHCDISTRATE");
        public string OVHDTRANS => LG_Firma_Tablosu("OVHDTRANS");
        public string OVRHDACCREF => LG_Firma_Tablosu("OVRHDACCREF");
        public string OVRHDCENTER => LG_Firma_Tablosu("OVRHDCENTER");
        public string OVRHDCENTERLN => LG_Firma_Tablosu("OVRHDCENTERLN");
        public string PARAMASGN => LG_Firma_Tablosu("PARAMASGN");
        public string PAYLINES => LG_Firma_Tablosu("PAYLINES");
        public string PAYPLANS => LG_Firma_Tablosu("PAYPLANS");
        public string PEGGING => LG_Firma_Tablosu("PEGGING");
        public string PERFTEST => LG_Firma_Tablosu("PERFTEST");
        public string POACCREF => LG_Firma_Tablosu("POACCREF");
        public string POLINE => LG_Firma_Tablosu("POLINE");
        public string PRCARDS => LG_Firma_Tablosu("PRCARDS");
        public string PRCLIST => LG_Firma_Tablosu("PRCLIST");
        public string PRCLSTDIV => LG_Firma_Tablosu("PRCLSTDIV");
        public string PREVDISPLINE => LG_Firma_Tablosu("PREVDISPLINE");
        public string PRINTLIMITS => LG_Firma_Tablosu("PRINTLIMITS");
        public string PRODORD => LG_Firma_Tablosu("PRODORD");
        public string PRODUCTLINE => LG_Firma_Tablosu("PRODUCTLINE");
        public string PROJECT => LG_Firma_Tablosu("PROJECT");
        public string PRVOPASG => LG_Firma_Tablosu("PRVOPASG");
        public string PURCHOFFER => LG_Firma_Tablosu("PURCHOFFER");
        public string PURCHOFFERLN => LG_Firma_Tablosu("PURCHOFFERLN");
        public string QASGN => LG_Firma_Tablosu("QASGN");
        public string QCLVAL => LG_Firma_Tablosu("QCLVAL");
        public string QCSET => LG_Firma_Tablosu("QCSET");
        public string QCSLINE => LG_Firma_Tablosu("QCSLINE");
        public string REFLECT => LG_Firma_Tablosu("REFLECT");
        public string REFLECTTRANS => LG_Firma_Tablosu("REFLECTTRANS");
        public string RELATEDDOCS => LG_Firma_Tablosu("RELATEDDOCS");
        public string REPAYPLAN => LG_Firma_Tablosu("REPAYPLAN");
        public string REPAYPLANS => LG_Firma_Tablosu("REPAYPLANS");
        public string REPAYPLANSLN => LG_Firma_Tablosu("REPAYPLANSLN");
        public string RETTAXPEGG => LG_Firma_Tablosu("RETTAXPEGG");
        public string ROUTE => LG_Firma_Tablosu("ROUTE");
        public string ROUTETRS => LG_Firma_Tablosu("ROUTETRS");
        public string ROUTING => LG_Firma_Tablosu("ROUTING");
        public string RTNGLINE => LG_Firma_Tablosu("RTNGLINE");
        public string SECTORMAIN => LG_Firma_Tablosu("SECTORMAIN");
        public string SECTORSUB => LG_Firma_Tablosu("SECTORSUB");
        public string SELCHVAL => LG_Firma_Tablosu("SELCHVAL");
        public string SHFTASGN => LG_Firma_Tablosu("SHFTASGN");
        public string SHFTTIME => LG_Firma_Tablosu("SHFTTIME");
        public string SHIFT => LG_Firma_Tablosu("SHIFT");
        public string SHIPINFO => LG_Firma_Tablosu("SHIPINFO");
        public string SLSCLREL => LG_Firma_Tablosu("SLSCLREL");
        public string SPECODES => LG_Firma_Tablosu("SPECODES");
        public string SPEVALLNSCORE => LG_Firma_Tablosu("SPEVALLNSCORE");
        public string SRVCARD => LG_Firma_Tablosu("SRVCARD");
        public string SRVUNITA => LG_Firma_Tablosu("SRVUNITA");
        public string STCOMPLN => LG_Firma_Tablosu("STCOMPLN");
        public string STDBOMCOST => LG_Firma_Tablosu("STDBOMCOST");
        public string STDCOST => LG_Firma_Tablosu("STDCOST");
        public string STDCOSTPERIOD => LG_Firma_Tablosu("STDCOSTPERIOD");
        public string STDUNITCOST => LG_Firma_Tablosu("STDUNITCOST");
        public string STOPASGN => LG_Firma_Tablosu("STOPASGN");
        public string STOPCAUSE => LG_Firma_Tablosu("STOPCAUSE");
        public string STOPTRANS => LG_Firma_Tablosu("STOPTRANS");
        public string SUPPASGN => LG_Firma_Tablosu("SUPPASGN");
        public string SUPPCRSET => LG_Firma_Tablosu("SUPPCRSET");
        public string SUPPCRSETLN => LG_Firma_Tablosu("SUPPCRSETLN");
        public string SUPPCRSETSUB => LG_Firma_Tablosu("SUPPCRSETSUB");
        public string SUPPEVALCR => LG_Firma_Tablosu("SUPPEVALCR");
        public string SUPPEVALCRLN => LG_Firma_Tablosu("SUPPEVALCRLN");
        public string SUPPEVALFICHE => LG_Firma_Tablosu("SUPPEVALFICHE");
        public string SUPPEVALTRANS => LG_Firma_Tablosu("SUPPEVALTRANS");
        public string SUPPEVALTRSET => LG_Firma_Tablosu("SUPPEVALTRSET");
        public string SUPPEVALTRSUB => LG_Firma_Tablosu("SUPPEVALTRSUB");
        public string TARGETS => LG_Firma_Tablosu("TARGETS");
        public string TAXDECLHDR => LG_Firma_Tablosu("TAXDECLHDR");
        public string TAXDECLLINE => LG_Firma_Tablosu("TAXDECLLINE");
        public string TAXDECLLINEACC => LG_Firma_Tablosu("TAXDECLLINEACC");
        public string TOOLREQ => LG_Firma_Tablosu("TOOLREQ");
        public string TRADGRPAYPLANCON => LG_Firma_Tablosu("TRADGRPAYPLANCON");
        public string TRGPAR => LG_Firma_Tablosu("TRGPAR");
        public string TSKSHELN => LG_Firma_Tablosu("TSKSHELN");
        public string UNITBARCODE => LG_Firma_Tablosu("UNITBARCODE");
        public string UNITSETC => LG_Firma_Tablosu("UNITSETC");
        public string UNITSETF => LG_Firma_Tablosu("UNITSETF");
        public string UNITSETL => LG_Firma_Tablosu("UNITSETL");
        public string UTILINVMTCH => LG_Firma_Tablosu("UTILINVMTCH");
        public string UTILINVMTCHLN => LG_Firma_Tablosu("UTILINVMTCHLN");
        public string VARIANT => LG_Firma_Tablosu("VARIANT");
        public string VEHICLECLSHIP => LG_Firma_Tablosu("VEHICLECLSHIP");
        public string VEHICLEWHOUSE => LG_Firma_Tablosu("VEHICLEWHOUSE");
        public string VRNTCHARASGN => LG_Firma_Tablosu("VRNTCHARASGN");
        public string VRNTGENERICINF => LG_Firma_Tablosu("VRNTGENERICINF");
        public string WFLOWROLE => LG_Firma_Tablosu("WFLOWROLE");
        public string WFLOWROLELN => LG_Firma_Tablosu("WFLOWROLELN");
        public string WFTASK => LG_Firma_Tablosu("WFTASK");
        public string WFTASKPER => LG_Firma_Tablosu("WFTASKPER");
        public string WHLIST => LG_Firma_Tablosu("WHLIST");
        public string WORKDAY => LG_Firma_Tablosu("WORKDAY");
        public string WORKFLOWCARD => LG_Firma_Tablosu("WORKFLOWCARD");
        public string WORKFLOWLINE => LG_Firma_Tablosu("WORKFLOWLINE");
        public string WORKSTAT => LG_Firma_Tablosu("WORKSTAT");
        public string WSATTASG => LG_Firma_Tablosu("WSATTASG");
        public string WSATTVAS => LG_Firma_Tablosu("WSATTVAS");
        public string WSCHCODE => LG_Firma_Tablosu("WSCHCODE");
        public string WSCHVAL => LG_Firma_Tablosu("WSCHVAL");
        public string WSGRPASS => LG_Firma_Tablosu("WSGRPASS");
        public string WSGRPF => LG_Firma_Tablosu("WSGRPF");
        public string WSOVHCASGN => LG_Firma_Tablosu("WSOVHCASGN");
        public string WSTATPART => LG_Firma_Tablosu("WSTATPART");
        public string PYWEBSIPARISLERMASTER => PY_Firma_Tablosu("SIPARISLER_MASTER");
        public string PYWEBSIPARISLERDETAIL => PY_Firma_Tablosu("SIPARISLER_DETAIL");

        #endregion

        #region Dönem Tabloları

        public string ACCDISTDETLN => LG_Donem_Tablosu("ACCDISTDETLN");
        public string ACCFCASGN => LG_Donem_Tablosu("ACCFCASGN");
        public string ANBDGTALLOCFC => LG_Donem_Tablosu("ANBDGTALLOCFC");
        public string ANBDGTALLOCLN => LG_Donem_Tablosu("ANBDGTALLOCLN");
        public string ANBDGTALLOCPRD => LG_Donem_Tablosu("ANBDGTALLOCPRD");
        public string ANBDGTREVFC => LG_Donem_Tablosu("ANBDGTREVFC");
        public string ANBDGTREVLN => LG_Donem_Tablosu("ANBDGTREVLN");
        public string ANBDGTREVPRD => LG_Donem_Tablosu("ANBDGTREVPRD");
        public string APPROVAL => LG_Donem_Tablosu("APPROVAL");
        public string APPROVE => LG_Donem_Tablosu("APPROVE");
        public string BNFICHE => LG_Donem_Tablosu("BNFICHE");
        public string BNFLINE => LG_Donem_Tablosu("BNFLINE");
        public string CLCOLLATRLRISK => LG_Donem_Tablosu("CLCOLLATRLRISK");
        public string CLFICHE => LG_Donem_Tablosu("CLFICHE");
        public string CLFLINE => LG_Donem_Tablosu("CLFLINE");
        public string CLPARAMHEADER => LG_Donem_Tablosu("CLPARAMHEADER");
        public string CLPARAMS => LG_Donem_Tablosu("CLPARAMS");
        public string CLRNUMS => LG_Donem_Tablosu("CLRNUMS");
        public string COLLATRLCARD => LG_Donem_Tablosu("COLLATRLCARD");
        public string COLLATRLROLL => LG_Donem_Tablosu("COLLATRLROLL");
        public string COLLATRLTRAN => LG_Donem_Tablosu("COLLATRLTRAN");
        public string COLLCOMMPAYTR => LG_Donem_Tablosu("COLLCOMMPAYTR");
        public string COSTDISTFC => LG_Donem_Tablosu("COSTDISTFC");
        public string COSTDISTLN => LG_Donem_Tablosu("COSTDISTLN");
        public string COSTDISTPEG => LG_Donem_Tablosu("COSTDISTPEG");
        public string CSCARD => LG_Donem_Tablosu("CSCARD");
        public string CSHTOTS => LG_Donem_Tablosu("CSHTOTS");
        public string CSROLL => LG_Donem_Tablosu("CSROLL");
        public string CSTRANS => LG_Donem_Tablosu("CSTRANS");
        public string DATAEXCHHISTORY => LG_Donem_Tablosu("DATAEXCHHISTORY");
        public string DEFNFLDSTRANV => LG_Donem_Tablosu("DEFNFLDSTRANV");
        public string DEMANDFICHE => LG_Donem_Tablosu("DEMANDFICHE");
        public string DEMANDLINE => LG_Donem_Tablosu("DEMANDLINE");
        public string DEMANDPEGGING => LG_Donem_Tablosu("DEMANDPEGGING");
        public string DIIB => LG_Donem_Tablosu("DIIB");
        public string DIIBBOMLINE => LG_Donem_Tablosu("DIIBBOMLINE");
        public string DIIBLINE => LG_Donem_Tablosu("DIIBLINE");
        public string DISCPAYTRANS => LG_Donem_Tablosu("DISCPAYTRANS");
        public string DISTORD => LG_Donem_Tablosu("DISTORD");
        public string DISTORDLINE => LG_Donem_Tablosu("DISTORDLINE");
        public string DIVATRANS => LG_Donem_Tablosu("DIVATRANS");
        public string DOCPRINT => LG_Donem_Tablosu("DOCPRINT");
        public string DOCSCHEMA => LG_Donem_Tablosu("DOCSCHEMA");
        public string DOCSELLIST => LG_Donem_Tablosu("DOCSELLIST");
        public string EARCHIVEDET => LG_Donem_Tablosu("EARCHIVEDET");
        public string EBOOKDETAILDOC => LG_Donem_Tablosu("EBOOKDETAILDOC");
        public string EINVOICEDET => LG_Donem_Tablosu("EINVOICEDET");
        public string EINVOICELOG => LG_Donem_Tablosu("EINVOICELOG");
        public string EMDEMDETLN => LG_Donem_Tablosu("EMDEMDETLN");
        public string EMDEMFICHE => LG_Donem_Tablosu("EMDEMFICHE");
        public string EMDEMFLINE => LG_Donem_Tablosu("EMDEMFLINE");
        public string EMFICHE => LG_Donem_Tablosu("EMFICHE");
        public string EMFLINE => LG_Donem_Tablosu("EMFLINE");
        public string EMFLNINFCOEF => LG_Donem_Tablosu("EMFLNINFCOEF");
        public string EPRODUCERRECDET => LG_Donem_Tablosu("EPRODUCERRECDET");
        public string ETRADESMANINVDET => LG_Donem_Tablosu("ETRADESMANINVDET");
        public string EXIMDISTFC => LG_Donem_Tablosu("EXIMDISTFC");
        public string EXIMDISTLN => LG_Donem_Tablosu("EXIMDISTLN");
        public string EXIMDISTPEG => LG_Donem_Tablosu("EXIMDISTPEG");
        public string EXIMHISTORY => LG_Donem_Tablosu("EXIMHISTORY");
        public string EXIMWHFC => LG_Donem_Tablosu("EXIMWHFC");
        public string EXIMWHTRANS => LG_Donem_Tablosu("EXIMWHTRANS");
        public string EXTRAINFO => LG_Donem_Tablosu("EXTRAINFO");
        public string FCACCREF => LG_Donem_Tablosu("FCACCREF");
        public string FICHEOBJECT => LG_Donem_Tablosu("FICHEOBJECT");
        public string FOLDER => LG_Donem_Tablosu("FOLDER");
        public string GNTOTBN => LG_Donem_Tablosu("GNTOTBN");
        public string GNTOTCSH => LG_Donem_Tablosu("GNTOTCSH");
        public string GUARANTOR => LG_Donem_Tablosu("GUARANTOR");
        public string HISTORY => LG_Donem_Tablosu("HISTORY");
        public string IMPSRVREL => LG_Donem_Tablosu("IMPSRVREL");
        public string INSTALCARD => LG_Donem_Tablosu("INSTALCARD");
        public string INVENVAL => LG_Donem_Tablosu("INVENVAL");
        public string INVENVALLN => LG_Donem_Tablosu("INVENVALLN");
        public string INVEXIMINFO => LG_Donem_Tablosu("INVEXIMINFO");
        public string INVEXIMLINES => LG_Donem_Tablosu("INVEXIMLINES");
        public string INVOICE => LG_Donem_Tablosu("INVOICE");
        public string INVOICEEXCH => LG_Donem_Tablosu("INVOICEEXCH");
        public string INVOICEINTEL => LG_Donem_Tablosu("INVOICEINTEL");
        public string ITMWSTOT => LG_Donem_Tablosu("ITMWSTOT");
        public string JOURNAL => LG_Donem_Tablosu("JOURNAL");
        public string KSDISTDETLINES => LG_Donem_Tablosu("KSDISTDETLINES");
        public string KSLINES => LG_Donem_Tablosu("KSLINES");
        public string LDXRECDELREQ => LG_Donem_Tablosu("LDXRECDELREQ");
        public string MBSCRMRELP => LG_Donem_Tablosu("MBSCRMRELP");
        public string MULTIADDTAXLN => LG_Donem_Tablosu("MULTIADDTAXLN");
        public string OKCINFO => LG_Donem_Tablosu("OKCINFO");
        public string ORDFEXCH => LG_Donem_Tablosu("ORDFEXCH");
        public string ORDLINEEXCH => LG_Donem_Tablosu("ORDLINEEXCH");
        public string ORDPEGGING => LG_Donem_Tablosu("ORDPEGGING");
        public string ORFICHE => LG_Donem_Tablosu("ORFICHE");
        public string ORFLINE => LG_Donem_Tablosu("ORFLINE");
        public string PACKAGEASGN => LG_Donem_Tablosu("PACKAGEASGN");
        public string PACKAGEFCLN => LG_Donem_Tablosu("PACKAGEFCLN");
        public string PACKAGEFICHE => LG_Donem_Tablosu("PACKAGEFICHE");
        public string PAYTRANS => LG_Donem_Tablosu("PAYTRANS");
        public string PERDOC => LG_Donem_Tablosu("PERDOC");
        public string PLUGINS => LG_Donem_Tablosu("PLUGINS");
        public string PRDCOST => LG_Donem_Tablosu("PRDCOST");
        public string PREACCDISTDETLINE => LG_Donem_Tablosu("PREACCDISTDETLINE");
        public string PROCESSLOG => LG_Donem_Tablosu("PROCESSLOG");
        public string PROCUREMENT => LG_Donem_Tablosu("PROCUREMENT");
        public string PRODUCER => LG_Donem_Tablosu("PRODUCER");
        public string QPRODLINE => LG_Donem_Tablosu("QPRODLINE");
        public string QPRODUCT => LG_Donem_Tablosu("QPRODUCT");
        public string REFLECTASGN => LG_Donem_Tablosu("REFLECTASGN");
        public string REMINDHIST => LG_Donem_Tablosu("REMINDHIST");
        public string RESPONSEHISTORY => LG_Donem_Tablosu("RESPONSEHISTORY");
        public string RULEHISTORY => LG_Donem_Tablosu("RULEHISTORY");
        public string RULES => LG_Donem_Tablosu("RULES");
        public string SERILOTN => LG_Donem_Tablosu("SERILOTN");
        public string SLQCASGN => LG_Donem_Tablosu("SLQCASGN");
        public string SLTRANS => LG_Donem_Tablosu("SLTRANS");
        public string SPECTEMPLATES => LG_Donem_Tablosu("SPECTEMPLATES");
        public string SRVNUMS => LG_Donem_Tablosu("SRVNUMS");
        public string STFCEXTINF => LG_Donem_Tablosu("STFCEXTINF");
        public string STFEXCH => LG_Donem_Tablosu("STFEXCH");
        public string STFICHE => LG_Donem_Tablosu("STFICHE");
        public string STINVENS => LG_Donem_Tablosu("STINVENS");
        public string STLINE => LG_Donem_Tablosu("STLINE");
        public string STLINECOST => LG_Donem_Tablosu("STLINECOST");
        public string STLINEEXCH => LG_Donem_Tablosu("STLINEEXCH");
        public string STLNINFCOEF => LG_Donem_Tablosu("STLNINFCOEF");
        public string STLNIOPEGGING => LG_Donem_Tablosu("STLNIOPEGGING");
        public string STSHIPPEDAMOUNT => LG_Donem_Tablosu("STSHIPPEDAMOUNT");
        public string TMPACASGN => LG_Donem_Tablosu("TMPACASGN");
        public string TRANSAC => LG_Donem_Tablosu("TRANSAC");
        public string VRNTINVENS => LG_Donem_Tablosu("VRNTINVENS");

        #endregion

        #region Viewler

        public string CLTOTFIL => LV_Donem_View("CLTOTFIL");
        public string EMUHTOT => LV_Donem_View("EMUHTOT");
        public string GNTOTCL => LV_Donem_View("GNTOTCL");
        public string GNTOTST => LV_Donem_View("GNTOTST");
        public string GNTOTVRNT => LV_Donem_View("GNTOTVRNT");
        public string SRVTOT => LV_Donem_View("SRVTOT");
        public string STINVTOT => LV_Donem_View("STINVTOT");
        public string VRNTINVTOT => LV_Donem_View("VRNTINVTOT");

        #endregion

        #region Tarih Saat

        public int GetLogoDate() { return GetLogoDate(DateTime.Today); }

        public int GetLogoDate(DateTime Tarih) { return (Tarih.Year * 65536) + (Tarih.Month * 256) + Tarih.Day; }

        public DateTime GetLogoDate(int LogoTarih)
        {
            int dYil = 0;
            int dAy = 0;
            int dGun = 0;

            dYil = (LogoTarih - (LogoTarih % 65536)) / 65536;
            LogoTarih = LogoTarih - (dYil * 65536);
            if (LogoTarih > 0)
            {
                dAy = (LogoTarih - (LogoTarih % 256)) / 256;
                dGun = LogoTarih - (dYil * 256);
            }

            return new DateTime(dYil, dAy, dGun);
        }

        public int GetLogoTime() { return GetLogoTime(DateTime.Now.TimeOfDay); }

        public int GetLogoTime(DateTime TarihSaat) { return GetLogoTime(TarihSaat.TimeOfDay); }

        public int GetLogoTime(TimeSpan Saat) { return (Saat.Hours * 16777216) + (Saat.Minutes * 65536) + (Saat.Seconds * 256); }

        public TimeSpan GetLogoTime(Int64 LogoSaat)
        {
            Int64 dSaat = 0;
            Int64 dDakika = 0;
            Int64 dSaniye = 0;

            dSaat = (LogoSaat - (LogoSaat % 65536)) / 65536 / 256;
            dDakika = ((LogoSaat - (LogoSaat % 65536)) / 65536 - ((LogoSaat - (LogoSaat % 65536)) / 65536 / 256) * 256);
            dSaniye = (((LogoSaat % 65536) - ((LogoSaat % 65536) % 256)) / 256);

            //dSaat = (LogoSaat - (LogoSaat % 16777216)) / 16777216;
            //LogoSaat = LogoSaat - (dSaat * 16777216);
            //if (LogoSaat > 0)
            //{
            //    dDakika = (LogoSaat - (LogoSaat % 65536)) / 65536;
            //    LogoSaat = LogoSaat - (dSaat * 65536);
            //    if (LogoSaat > 0)
            //        dSaniye = (LogoSaat - (LogoSaat % 256)) / 256;
            //}

            return new TimeSpan(0, (int)dSaat, (int)dDakika, (int)dSaniye, 0);
        }

        #endregion

        #region HelperFunctions

        public List<string> SplitSpaceCharArray(string text, int Length)
            => SplitCharArray(text, ' ', Length);

        public List<string> SplitCharArray(string text, Char splitchar, int Length)
        {
            List<string> lstResult = new List<string>();
            if (text.Length > Length)
            {
                List<string> lsttext = text.Split(splitchar).ToList();
                while (lsttext.Count > 0)
                {
                    if (lstResult.Count > 0 && lstResult[lstResult.Count - 1].Length < Length)
                    {
                        if (lstResult[lstResult.Count - 1].Length + lsttext[0].Length < Length)
                            lstResult[lstResult.Count - 1] += splitchar + lsttext[0];
                        else
                            lstResult.Add(lsttext[0]);
                    }
                    else
                        lstResult.Add(lsttext[0]);
                    lsttext.RemoveAt(0);
                }
            }
            else
                lstResult.Add(text);


            return lstResult;
        }

        #endregion

        public string LG_Firma_Tablosu(string TabloAd)
        {
            string Firma = Settings.FirmaNo.ToString();
            return LG_Firma_Tablosu(Firma, TabloAd);
        }

        public string LG_Firma_Tablosu(string Firma, string TabloAd)
            => string.Format("{0}{1}_{2}_{3}", DBPrefixFromFirm(Convert.ToInt32(Firma)), "LG", Firma.ToString().PadLeft(3, '0'), TabloAd);

        public string DBPrefixFromFirm(int PrefixFirmaNo)
        {
            string query = @"SELECT * FROM L_CAPIFIRM";

            DataTable dtERPFirmalar = new DataTable();
            string sqlDataSource = Settings.ConnectionString;
            SqlDataReader mySqlDataReader;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    mySqlDataReader = cmd.ExecuteReader();
                    dtERPFirmalar.Load(mySqlDataReader);

                    mySqlDataReader.Close();
                    conn.Close();
                }
            }

            string strPrefix = "";
            if (dtERPFirmalar != null && dtERPFirmalar.Select("NR = " + PrefixFirmaNo).Length > 0)
                strPrefix = dtERPFirmalar.Select("NR = " + PrefixFirmaNo)[0]["DBNAME"].ToString();

            if (strPrefix != "")
                strPrefix = string.Format("[{0}].dbo.", strPrefix);
            else
                strPrefix = "";

            return strPrefix;
        }

        public string LG_Donem_Tablosu(string TabloAd)
        {
            string Firma = Settings.FirmaNo.ToString();
            string Donem = Settings.DonemNo.ToString();
            return LG_Donem_Tablosu(Firma, Donem, TabloAd);
        }

        public string LG_Donem_Tablosu(string Firma, string Donem, string TabloAd)
        {
            return string.Format("{0}{1}_{2}_{3}_{4}", DBPrefixFromFirm(Convert.ToInt32(Firma)), "LG", Firma.ToString().PadLeft(3, '0'), Donem.ToString().PadLeft(2, '0'), TabloAd);
        }

        public string LV_Donem_View(string ViewAd)
        {
            string Firma = Settings.FirmaNo.ToString();
            string Donem = Settings.DonemNo.ToString();
            return LV_Donem_View(Firma, Donem, ViewAd);
        }

        public string LV_Donem_View(string Firma, string Donem, string ViewAd)
        {
            return string.Format("{0}LV_{1}_{2}_{3}", DBPrefixFromFirm(Convert.ToInt32(Firma)), Firma.ToString().PadLeft(3, '0'), Donem.ToString().PadLeft(2, '0'), ViewAd);
        }

        public string PY_Firma_Tablosu(string TabloAd)
        {
            string Firma = Settings.FirmaNo.ToString();
            return PY_Firma_Tablosu(Firma, TabloAd);
        }

        public string PY_Firma_Tablosu(string Firma, string TabloAd)
            => string.Format("{0}{1}_{2}_{3}", DBPrefixFromFirm(Convert.ToInt32(Firma)), "PY_WEB", Firma.ToString().PadLeft(3, '0'), TabloAd);

        #region DOVIZ
        public Double GetCurrRateFirmExChange(int DovizTuru, int KurTipi)
            => GetCurrRateFirmExChange(DovizTuru, DateTime.Today, KurTipi);
        public Double GetCurrRateFirmExChange(int DovizTuru, DateTime Tarih, int KurTipi)
            => GetCurrRateFirmExChangeBase(DovizTuru, Tarih, KurTipi);
        public Double GetCurrRateDailyExChange(int DovizTuru, int KurTipi)
            => GetCurrRateDailyExChange(DovizTuru, DateTime.Today, KurTipi);
        public Double GetCurrRateDailyExChange(int DovizTuru, DateTime Tarih, int KurTipi)
            => GetCurrRateDailyExChangeBase(DovizTuru, Tarih, KurTipi);
        public Double GetCurrRate(int DovizTuru, DateTime Tarih, int KurTipi)
        {
            return Erp_Ayri_Doviz_Tablosu ? GetCurrRateFirmExChange(DovizTuru, Tarih, KurTipi) : GetCurrRateDailyExChange(DovizTuru, Tarih, KurTipi);
        }
        public Double GetCurrRateFirmExChangeBase(int DovizTuru, DateTime Tarih, int KurTipi)
     => GetCurrRateExChangeBase(string.Format("LG_EXCHANGE_{0}", Settings.FirmaNo), DovizTuru, Tarih, KurTipi);

        public Double GetCurrRateDailyExChangeBase(int DovizTuru, DateTime Tarih, int KurTipi)
             => GetCurrRateExChangeBase("L_DAILYEXCHANGES", DovizTuru, Tarih, KurTipi);

        public Double GetCurrRateExChangeBase(string TableName, int DovizTuru, DateTime Tarih, int KurTipi)
        {
            Double dResult = 0;

            using (SqlConnection conn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "SELECT * FROM " + TableName + " WHERE EDATE = @_TARIH AND CRTYPE = @_DOVIZ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@_TARIH", Tarih);
                    cmd.Parameters.AddWithValue("@_DOVIZ", DovizTuru);
                    using (DataTable dtTemp = this.Execute(cmd))
                    {
                        if (dtTemp.Rows.Count > 0 && dtTemp.Columns["RATES" + KurTipi] != null && dtTemp.Rows[0]["RATES" + KurTipi] != DBNull.Value)
                            dResult = Convert.ToDouble(dtTemp.Rows[0]["RATES" + KurTipi]);
                    }

                    return dResult;
                }
            }
            //var command = BuildCommand("SELECT * FROM " + TableName + " WHERE EDATE = @_TARIH AND CRTYPE = @_DOVIZ");
            //this.AddDateParameter(command, "@_TARIH", Tarih);
            //this.AddIntParameter(command, "@_DOVIZ", DovizTuru);
            
        }
        public bool Erp_Ayri_Doviz_Tablosu { get { return (drAktifFirma != null ? (drAktifFirma["SEPEXCHTABLE"].ToString().Equals("0") ? false : true) : false); } }
        internal DataRow drAktifFirma
        {
            get
            {
                DataRow drresult = null;
                string query = @"SELECT * FROM L_CAPIFIRM";
                DataTable dtERPFirmalar = new DataTable();
                string sqlDataSource = Settings.ConnectionString;
                SqlDataReader mySqlDataReader;
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        mySqlDataReader = cmd.ExecuteReader();
                        dtERPFirmalar.Load(mySqlDataReader);

                        mySqlDataReader.Close();
                        conn.Close();
                    }
                }
                if (dtERPFirmalar != null &&
                    dtERPFirmalar.Select("NR = " + Settings.FirmaNo).Length > 0)
                    drresult = dtERPFirmalar.Select("NR = " + Settings.FirmaNo)[0];

                return drresult;
            }
        }

        public SqlCommand BuildCommand(string sql)
        {
            SqlCommand command;
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
            };

            return command;
        }
        private void AddDateParameter(SqlCommand command, string parameterName, DateTime value)
        {
            command.Parameters.Add(new SqlParameter(parameterName, SqlDbType.DateTime) { Value = value });
        }
        private void AddIntParameter(SqlCommand command, string parameterName, int value)
        {
            command.Parameters.Add(new SqlParameter(parameterName, SqlDbType.Int) { Value = value });
        }

        private DataTable Execute(SqlCommand command)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        #endregion
    }
}
