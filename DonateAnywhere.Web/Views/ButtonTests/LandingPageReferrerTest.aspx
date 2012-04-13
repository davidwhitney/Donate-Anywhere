<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>

<a href="<%=Model.SiteRoot %>LandingPage">Go to landing page...</a><br /><br />


When you click the link to the landing page above, the landing page will spider this referring Url and respond with some contextual data.<br />
In this case, it should pick up on keywords related to pigs and their size.<br /><br />

<p>
The ASA said the farm had not substantiated its claim of pigs growing no more than 12in to 16in and banned the ad for misleading consumers. However, it dismissed the complaint that the pigs were not "easy to care for", saying readers "would understand there was a certain amount of work and effort involved in caring for the little pigs, irrespective of their eventual size".</p>
</body>
</html>
