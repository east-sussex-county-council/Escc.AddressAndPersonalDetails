<?xml version="1.0" encoding="UTF-8"?>
<!--
 Author: Rick Mason, Web Team
 Date: 23 June 2010
 Description: Feed a BS7666 address in as a parameter, and get back a Simple Address. Uses an XSLT provided by central government as
              part of the Address and Personal Details 2.0 Schema Library to apply the rules for transforming to a UK Postal Address
              (ie: Simple Address represented in XML). Stylesheet downloaded from http://www.cabinetoffice.gov.uk/media/259318/BS7666toUKpostalAddress.xml
-->
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:bs7666="http://www.govtalk.gov.uk/people/bs7666"
	xmlns:apd="http://www.govtalk.gov.uk/people/AddressAndPersonalDetails"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:include href="Escc.AddressAndPersonalDetails.BS7666ToUKPostalAddress.xslt"/>

  <!-- Convert a BS7666 address to a Simple Address -->
<xsl:template name="BS7666AddressToSimpleAddress">
  <xsl:param name="BS7666Address" />
  <xsl:param name="Separator" />

  <!-- First, use the BS7666ToUKPostalAddress stylesheet to convert to an XML version of Simple Address. -->
  <xsl:variable name="PostalAddress">
    <xsl:apply-templates select="$BS7666Address" />
  </xsl:variable>

  <!-- Now take that XML simple address and make it a text string. Unfortunately had to use function specific to MS parser as couldn't find alternative. -->
  <xsl:for-each select="msxsl:node-set($PostalAddress)/apd:UKPostalAddress">
    <xsl:variable name="HasPostcode" select="count(apd:PostCode) > 0" />
    <xsl:variable name="NumLines" select="count(apd:Line)" />

    <xsl:for-each select="apd:Line">
      <xsl:value-of select="."/>
      <xsl:if test="$NumLines > position()">
        <xsl:value-of select="$Separator"/>
      </xsl:if>
      <xsl:if test="$NumLines = position() and $HasPostcode = 'true'">
        <xsl:text> </xsl:text>
      </xsl:if>
    </xsl:for-each>
    <xsl:value-of select="apd:PostCode"/>
  </xsl:for-each>
</xsl:template>

</xsl:stylesheet>
