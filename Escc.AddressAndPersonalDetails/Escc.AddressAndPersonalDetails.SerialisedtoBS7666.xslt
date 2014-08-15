<?xml version="1.0" encoding="utf-8"?>
<!-- 
Author:      Rick Mason, ESCC web team
Date:        12 Feb 2011
Description: This stylesheet can be used to convert the default .NET serialisation of the Escc.AddressAndPersonalDetails.BS7666Address 
             class to a representation using the official BS7666 XML schema. It can be used by passing an AddressElement 
             parameter with the name of the address element (but not the path), or by inclusion in another XSL stylesheet 
             where the BS7666Address named template can be called on any element. 
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
    xmlns:bs7666="http://www.govtalk.gov.uk/people/bs7666"
    xmlns:gdsc="http://eastsussex.gov.uk/Escc.AddressAndPersonalDetails"
>
  <!-- Turn indenting off to avoid generating empty text nodes inside elements with no content -->
  <xsl:output method="xml" indent="no"/>

  <!-- Copy the namespace declarations from the root of this stylesheet into the root of the transform output,
       which means the bs7666 and gdsc prefixes will be defined correctly -->
  <xsl:template match="/*">
    <xsl:copy>
      <xsl:copy-of select="document('')/xsl:stylesheet/namespace::*[not(local-name() = 'xsl') and not(local-name() = 'msxsl')]"/>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <!-- Leave everything unaltered apart from the specific element specified by the AddressElement parameter -->
  <xsl:param name="AddressElement" />
  <xsl:template match="@* | node()">
    <xsl:choose>
      <xsl:when test="name() = $AddressElement">
        <xsl:call-template name="BS7666Address" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:copy>
          <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
 
  <!-- Template to match a serialised Escc.AddressAndPersonalDetails.BS7666Address class and transform it to BS7666-compliant XML.
       As an alternative to calling this XSLT using the AddressElement parameter, you can xsl:include it inside
       another XSLT file and call this template directly on a specific element using <xsl:call-template name="BS7666Address" />. 
       Use this approach if you need to specify the path to the element, which you can't specify using AddressElement. -->
  <xsl:template name="BS7666Address">
    <xsl:choose>
      <xsl:when test="string-length(Id) or string-length(GridEasting) or string-length(GridNorthing) or string-length(Saon) or string-length(Paon) or string-length(StreetName) or string-length(Usrn) or string-length(Locality) or string-length(Town) or string-length(AdministrativeArea) or string-length(Postcode) or string-length(Uprn)">

        <xsl:element name="{name()}">
      <!-- Write an element for the id if there is an id. Not a BS7666 property so do it in our namespace,
           and then write the BS7666 element as a sibling of id.-->
      <xsl:if test="Id > 0">
        <xsl:element name="gdsc:AddressId">
          <xsl:value-of select="Id" />
        </xsl:element>
      </xsl:if>

      <!-- Write elements for easting and northing if present. BS7666 has a type for this, but it's part of the Basic Land and Property Unit (BLPU)
           and there's lots more information in a BLPU than we have in this class. So we can't represent easting and northing using the correct
           namespaced elements and expect it to validate, but we can at least use the same elements but in our namespace to be consistent.
           Avoid serialising a location of 0, 0 which is a valid location at the corner of a grid square but is probably wrong.-->
      <xsl:if test="GridEasting > 0 and GridNorthing > 0">
        <xsl:element name="gdsc:GridReference">
          <xsl:element name="gdsc:X">
            <xsl:value-of select="GridEasting" />
          </xsl:element>
          <xsl:element name="gdsc:Y">
            <xsl:value-of select="GridNorthing" />
          </xsl:element>
        </xsl:element>  
      </xsl:if>

      <!-- Check if we have any components of a BS7666 address before writing it -->
      <xsl:if test="string-length(Saon) or string-length(Paon) or string-length(StreetName) or string-length(Usrn) or string-length(Locality) or string-length(Town) or string-length(AdministrativeArea) or string-length(Postcode) or string-length(Uprn)">
        
        <!-- Write a BS7666 element for the address itself. This is the start of the conforming, validatable BS7666 format -->
        <bs7666:BS7666Address xsi:schemaLocation="http://www.govtalk.gov.uk/people/bs7666 http://interim.cabinetoffice.gov.uk/media/263510/bs7666-v1-4.xsd">

          <!-- SAON is optional
                TODO: XML schema also supports specifying StartRange with a suffix and an EndRange element for building numbers, in which case description is optional, but this code doesn't support that yet -->
          <xsl:if test="string-length(Saon)">
            <bs7666:SAON>
              <xsl:choose>
                <xsl:when test="string-length(Saon) &lt;= 4 and number(Saon)">
                  <bs7666:StartRange>
                    <xsl:value-of select="Saon" />
                  </bs7666:StartRange>
                </xsl:when>
                <xsl:otherwise>
                  <bs7666:Description>
                    <xsl:value-of select="Saon" />
                  </bs7666:Description>
                </xsl:otherwise>
              </xsl:choose>
              </bs7666:SAON>
          </xsl:if>

          <!-- PAON is a required element, and must not be empty
               TODO: XML schema also supports specifying StartRange with a suffix and an EndRange element for building numbers, in which case description is optional, but this code doesn't support that yet -->
          <bs7666:PAON>

              <xsl:choose>
                <xsl:when test="string-length(Paon)">
                  <xsl:choose>
                    <xsl:when test="string-length(Paon) &lt;= 4 and number(Paon)">
                      <bs7666:StartRange>
                        <xsl:value-of select="Paon" />
                      </bs7666:StartRange>
                    </xsl:when>
                    <xsl:otherwise>
                      <bs7666:Description>
                        <xsl:value-of select="Paon" />
                      </bs7666:Description>                    
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:when>
                <xsl:otherwise>
                  <bs7666:Description>
                    <xsl:text> </xsl:text>
                  </bs7666:Description>
                </xsl:otherwise>
              </xsl:choose>
            </bs7666:PAON>

          <!-- Street description is required, and must not be empty -->
          <bs7666:StreetDescription>
            <xsl:choose>
              <xsl:when test="string-length(StreetName)">
                <xsl:value-of select="StreetName" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:text> </xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </bs7666:StreetDescription>

          <!-- Unique Street Reference Number is optional -->
          <xsl:if test="string-length(Usrn)">
            <bs7666:UniqueStreetReferenceNumber>
              <xsl:value-of select="Usrn" />
            </bs7666:UniqueStreetReferenceNumber>
          </xsl:if>

          <!-- Locality is optional -->
          <xsl:if test="string-length(Locality)">
            <bs7666:Locality>
              <xsl:value-of select="Locality" />
            </bs7666:Locality>
          </xsl:if>

          <!-- Town is optional -->
          <xsl:if test="string-length(Town)">
            <bs7666:Town>
              <xsl:value-of select="Town" />
            </bs7666:Town>
          </xsl:if>

          <!-- Administrative area is optional so long as there's a locality or town, but it must not be empty -->
          <xsl:choose>
            <xsl:when test="string-length(AdministrativeArea)">
              <bs7666:AdministrativeArea>
                <xsl:value-of select="AdministrativeArea" />
              </bs7666:AdministrativeArea>
            </xsl:when>
            <xsl:otherwise>
              <xsl:if test="string-length(Locality) and string-length(Town)">
                <bs7666:AdministrativeArea>
                  <xsl:text> </xsl:text>
                </bs7666:AdministrativeArea>
              </xsl:if>
            </xsl:otherwise>
          </xsl:choose>

          <!-- Postcode is optional -->
          <xsl:if test="string-length(Postcode)">
            <bs7666:PostCode>
              <xsl:value-of select="Postcode" />
            </bs7666:PostCode>
          </xsl:if>
          <xsl:copy-of select="Uprn"></xsl:copy-of>
        </bs7666:BS7666Address>

        <!-- Unique Property Reference Number is optional -->
        <xsl:if test="string-length(Uprn)">
          <bs7666:UniquePropertyReferenceNumber>
            <xsl:value-of select="Uprn" />
          </bs7666:UniquePropertyReferenceNumber>
        </xsl:if>
      </xsl:if>
    </xsl:element>
      </xsl:when>
      <xsl:otherwise>
        <xsl:element name="{name()}" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
</xsl:stylesheet>
